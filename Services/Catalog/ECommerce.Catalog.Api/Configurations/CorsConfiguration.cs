using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.Api.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("development", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });

                options.AddPolicy("staging", builder =>
                {
                    builder.WithOrigins("https://ecommerce-app.azurewebsites.net");
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });

                options.AddPolicy("production", builder =>
                {
                    builder.WithOrigins("https://ecommerce-app.azurewebsites.net");
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

        }
    }
}
