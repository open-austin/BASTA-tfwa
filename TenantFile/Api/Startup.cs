using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TenantFile.Api.Models.Tenants;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Tenants;
using TenantFile.Api.Models.Phones;
using TenantFile.Api.Models.Properties;
using TenantFile.Api.Models.Residences;
using TenantFile.Api.Models.Images;
using TenantFile.Api.Models.Addresses;
using HotChocolate.Execution.Options;
using TenantFile.Api.Models.Entities;
//using TenantFile.Api.Models.ImageLabels;

namespace TenantFile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var connectionString =
                new NpgsqlConnectionStringBuilder(
                    Configuration["LocalSQL:ConnectionString"])
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

            services.AddPooledDbContextFactory<TenantFileContext>(options => options.UseNpgsql(Configuration["LocalSQL:ConnectionString"])
            //.UseSnakeCaseNamingConvention()
            .LogTo(Console.WriteLine, LogLevel.Information))

            .AddGraphQLServer()
                    .AddApolloTracing(TracingPreference.Always)
                     .AddMutationType(d => d.Name("Mutation"))
                        .AddType<ImageMutations>()
                        .AddType<TenantMutations>()
                        .AddType<PhoneMutations>()
                        .AddType<PropertyMutations>()
                        .AddType<ResidenceMutations>()
                    .AddQueryType(d => d.Name("Query"))
                        .AddType<TenantQueries>()
                        .AddType<PropertyQueries>()
                        .AddType<ResidenceQueries>()
                        .AddType<PhoneQueries>()
                        .AddType<ImageQueries>()
                        //.AddType<ImageLabelQueries>()
                        .AddType<AddressQueries>()
                    .AddType<PhoneType>()
                    .AddType<TenantType>()
                    .AddType<AddressType>()
                    .AddType<ImageType>()
                    .AddType<PropertyType>()
                    .AddType<ResidenceType>()
                    //.AddType<ImageLabelType>()
                    .EnableRelaySupport()
                    .AddInMemorySubscriptions()
                    .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddType<PhoneSubscriptions>()
                    .AddDataLoader<TenantByIdDataLoader>()
                    .AddDataLoader<PhoneByIdDataLoader>()
                    .AddDataLoader<PropertyByIdDataLoader>()
                    .AddDataLoader<ResidenceByIdDataLoader>()
                    .AddDataLoader<ImageByIdDataLoader>()
                    .AddDataLoader<AddressByIdDataLoader>()
                    //.AddDataLoader<DataLoaderById<Address>>()
                    .AddAuthorization()
                    .AddFiltering()
                    .AddSorting()
                    .AddProjections()
                    ;



            services.AddSingleton<IAddressVerificationService>(s => new AddressVerificationService(Configuration["USPSUserName"]));
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
               options => options.SetIsOriginAllowed(x => _ = true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
            );

            //app.UseTwilioToStorage();
            app.UseWebSockets()
               .UseRouting()
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

                endpoints.MapGraphQL(); //replaces app.UseGraphQL()

                endpoints.MapGet("/",  context =>
                {

                    //await context.WebSockets.AcceptWebSocketAsync();  
                    //context.Response.Redirect("/playground");
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
