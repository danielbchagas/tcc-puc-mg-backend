using ECommerce.Pedido.Api.Configurations;
using ECommerce.Pedido.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace ECommerce.Pedido.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true);

            if (environment.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwtConfiguration(Configuration);
            services.AddEntityFrameworkConfiguration(Configuration);
            services.AddHealthCheckConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddOptionsConfiguration(Configuration);

            services.AddControllers().AddJsonOptions(
                opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }
            );

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerConfiguration();
            }
            else
            {
                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}