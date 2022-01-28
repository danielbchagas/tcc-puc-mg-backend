using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Gateway.Api.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("development", builder =>
                {
                    builder.WithOrigins("http://localhost", "https://localhost", "https://ecommerce-web-app.azurewebsites.net");
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });

                options.AddPolicy("staging", builder =>
                {
                    builder.WithOrigins("https://ecommerce-web-app.azurewebsites.net");
                });

                options.AddPolicy("production", builder =>
                {
                    builder.WithOrigins("https://ecommerce-web-app.azurewebsites.net");
                });
            });

        }
    }
}
