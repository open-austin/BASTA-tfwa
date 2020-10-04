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
using Microsoft.EntityFrameworkCore;


namespace TenantFile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefault()
            });

            services.AddDbContext<TenantContext>(options => options.UseNpgsql(Configuration.GetValue<string>("DbUrl"))
                    .UseSnakeCaseNamingConvention());

            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddCors();
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
            services.AddDataLoaderRegistry();

            // Add GraphQL Services
            services.AddGraphQL(
                SchemaBuilder.New()
                    // .EnableRelaySupport()
                    // enable for authorization support
                    .AddAuthorizeDirectiveType()
                    .AddQueryType<Models.Query>()
                    .AddMutationType<Mutation>().Create(), new QueryExecutionOptions { ForceSerialExecution = true });

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
               .UseGraphQL()
               .UsePlayground()
               .UseVoyager();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context =>
                // {
                //     var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";
                //     await context.Response.WriteAsync($"Hello {target}!\n");
                // });
                endpoints.MapControllers();
            });
        }
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<TenantContext>();
            if (context.Database.EnsureCreated())
            {

                context.SaveChangesAsync();
            }
        }
    }
}
