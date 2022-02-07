using System.Text.Json.Serialization;
using ECommerce.Basket.Api.Configurations;
using ECommerce.Basket.Api.Middlewares;
using ECommerce.Core.Apis.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Basket.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration.GetSection("JwtOptions").Get<ECommerce.Core.Apis.Options.Jwt>());
            services.AddDependencyInjectionConfiguration();
            services.AddEntityFrameworkConfiguration(Configuration);
            services.AddSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());
            
            services.AddControllers();

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

                app.UseSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());
            }
            else
            {
                app.UseSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
