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
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.EntityFrameworkCore;
using TenantFile.Api.Models.Tenants;
using TenantFile.Api.DataLoader;
using TenantFile.Api.Tenants;
using TenantFile.Api.Models.Phones;
using TenantFile.Api.Models.Properties;
using TenantFile.Api.Models.Residences;
using TenantFile.Api.Models.Images;
using TenantFile.Api.Models.Addresses;
using HotChocolate.Execution.Options;
using Npgsql;

namespace TenantFile.Api
{
  public class Startup
  {
    public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, IConfiguration configuration)
    {
      Configuration = configuration;
      _currentEnvironment = env;
    }

    public IConfiguration Configuration { get; }
    private readonly IWebHostEnvironment _currentEnvironment;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      FirebaseApp.Create(new AppOptions()
      {
        Credential = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefault()
      });
      services.AddScoped<TenantFileContext>();

      // If we are in development, we want to use our dockerized postgres which has known parameters.
      // Otherwise, we use the connection that will be fed to us by gcloud (don't question the name, we'll fix that
      // later
      // TODO: Get a better configuration name
      var connectionString = "";
      if (_currentEnvironment.IsDevelopment())
      {
        connectionString = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=example";
      } else if (_currentEnvironment.IsProduction())
      {
        connectionString = Configuration["LocalSQL:ConnectionString"];
      }

      services.AddPooledDbContextFactory<TenantFileContext>(options => options.UseNpgsql(connectionString)
              .LogTo(Console.WriteLine, LogLevel.Information))
        .AddGraphQLServer()
              .AddApolloTracing(TracingPreference.Always)
               .AddMutationType(d => d.Name("Mutation"))
                  .AddType<ImageMutations>()
                  .AddType<TenantMutations>()
                  .AddType<PhoneMutations>()
                  .AddType<PropertyMutations>()
                  .AddType<ResidenceMutations>()
                  .AddType<AddressMutations>()
              .AddQueryType(d => d.Name("Query"))
                  .AddType<TenantQueries>()
                  .AddType<PropertyQueries>()
                  .AddType<ResidenceQueries>()
                  .AddType<PhoneQueries>()
                  .AddType<ImageQueries>()
                  .AddType<AddressQueries>()
              .AddType<PhoneType>()
              .AddType<TenantType>()
              .AddType<AddressType>()
              .AddType<ImageType>()
              .AddType<PropertyType>()
              .AddType<ResidenceType>()
              .AddInMemorySubscriptions()
              .AddSubscriptionType(d => d.Name("Subscription"))
                  .AddType<PhoneSubscriptions>()
              .AddDataLoader<TenantByIdDataLoader>()
              .AddDataLoader<PhoneByIdDataLoader>()
              .AddDataLoader<PropertyByIdDataLoader>()
              .AddDataLoader<ResidenceByIdDataLoader>()
              .AddDataLoader<ImageByIdDataLoader>()
              .AddDataLoader<AddressByIdDataLoader>()
              .EnableRelaySupport()
              .AddAuthorization()
              .AddFiltering()
              .AddSorting()
              .AddProjections()
              ;

      services.AddSingleton<IAddressVerificationService>(s => new AddressVerificationService(Configuration["USPSUserName"]));
      services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
      services.AddScoped<GoogleDriveService>();
      services.AddCors();

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
      app.UseWebSockets()
         .UseRouting()
         .UsePlayground()
         .UseVoyager();

      app.UseAuthentication();
      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGraphQL(); //replaces app.UseGraphQL()

        endpoints.MapGet("/", context => Task.CompletedTask);
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
