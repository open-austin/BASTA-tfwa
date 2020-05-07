using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TenantFile.Api.Services;

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
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                              builder =>
                              {
                                  builder.WithOrigins("https://tenant-file-fc6de.firebaseapp.com",
                                                      "http://localhost:3000",
                                                      "http://api.tfwa.jacobcasper.com",
                                                      "http://tfwa.jacobcasper.com",
                                                      "https://api.tfwa.jacobcasper.com",
                                                      "https://tfwa.jacobcasper.com")
                                                      .AllowAnyMethod()
                                                      .AllowAnyHeader();
                              });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";
                    await context.Response.WriteAsync($"Hello {target}!\n");
                });
                endpoints.MapControllers();
            });
        }
    }
}
