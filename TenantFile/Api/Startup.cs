using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TenantFile.Api.Models;
using TenantFile.Api.Services;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using HotChocolate.Data.Sorting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TenantFile.Api.Middleware;
using TenantFile.Api.Models.Tenants;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Tenants;

namespace TenantFile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var connectionString = 
                new NpgsqlConnectionStringBuilder(
                    Configuration["LocalSql:ConnectionString"])
                          {
                        // Connecting to a local proxy that does not support ssl.
                        SslMode = SslMode.Disable
                        };
            NpgsqlConnection connection =
                new NpgsqlConnection(connectionString.ConnectionString);
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefault()
            });

            services.AddPooledDbContextFactory<TenantFileContext>(options => options.UseNpgsql(Configuration["LocalSql:ConnectionString"]).UseSnakeCaseNamingConvention())
            .AddGraphQLServer()
                .AddMutationType(d => d.Name("Mutation"))
                        .AddType<TenantMutations>()
                    // .EnableRelaySupport()
                    // enable for authorization support
                    .AddQueryType(d => d.Name("Query"))
                        .AddType<TenantQueries>()
                    .AddDataLoader<TenantByIdDataLoader>()
                    //.AddAuthorization()
                    //.AddFiltering()
                    .AddSorting();
                    //.EnableRelaySupport();
                    


            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddCors();

          #region old commentted code  
            // services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
            //    builder.WithOrigins("https://tenant-file-fc6de.firebaseapp.com",
            //                                 "http://localhost:3000",
            //                                 "http://api.tfwa.jacobcasper.com",
            //                                 "http://tfwa.jacobcasper.com",
            //                                 "https://api.tfwa.jacobcasper.com",
            //                                 "https://tfwa.jacobcasper.com")
            //     .AllowAnyMethod()
            //     .AllowAnyHeader().AllowCredentials()));
            // services.AddCors(options =>
            // {
            //     options.AddDefaultPolicy(
            //         builder =>
            //         {
            //             builder.WithOrigins("https://tenant-file-fc6de.firebaseapp.com",
            //                                 "http://localhost:3000",
            //                                 "http://api.tfwa.jacobcasper.com",
            //                                 "http://tfwa.jacobcasper.com",
            //                                 "https://api.tfwa.jacobcasper.com",
            //                                 "https://tfwa.jacobcasper.com")
            //                                 .AllowAnyMethod()
            //                                 .AllowAnyHeader();
            //         });
            // });
            #endregion
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = Configuration["GoogleProjectId"];
                options.Authority = $"https://securetoken.google.com/{Configuration["GoogleProjectId"]}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
            });

            // this enables you to use DataLoader in your resolvers.
            //services.AddDataLoaderRegistry();

            // Add GraphQL Services
        

           
            //, new QueryExecutionOptions { ForceSerialExecution = true } )// pooled dbContext is the solution replacing this;

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app);

            app.UseCors(
               options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            app.UseRouting()
               .UseWebSockets()
               //.UseGraphQL()//obsolete
               .UsePlayground()
               .UseVoyager();

            app.UseTwilioToStorage();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context =>
                // {
                //     var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";
                //     await context.Response.WriteAsync($"Hello {target}!\n");
                // });

                endpoints.MapGraphQL(); //replaces app.UseGraphQL()

                endpoints.MapGet("/", context =>
                {
                    // context.Response.Redirect("/playground");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using IServiceScope? serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

            var factory = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<TenantFileContext>>();
            using TenantFileContext dbContext = factory.CreateDbContext();

            if (dbContext.Database.EnsureCreated())
            {

                dbContext.SaveChangesAsync();
            }
        }
    }
}
