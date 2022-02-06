using ECommerce.Core.Apis.Configurations;
using ECommerce.Ordering.Gateway.Configurations;
using ECommerce.Ordering.Gateway.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Ordering.Gateway
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
            services.AddRefitConfiguration(Configuration);
            services.AddDependencyInjectionConfiguration();
            services.AddIdentityConfiguration(Configuration.GetSection("JwtOptions").Get<ECommerce.Core.Apis.Options.Jwt>());
            services.AddSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());
            services.AddOptionsConfiguration(Configuration);
            services.AddCorsConfiguration();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());

                app.UseCors("development");
            }
            else
            {
                app.UseSwaggerConfiguration(Configuration.GetSection("SwaggerOptions").Get<ECommerce.Core.Apis.Options.Swagger>());

                app.UseCors("production");
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
