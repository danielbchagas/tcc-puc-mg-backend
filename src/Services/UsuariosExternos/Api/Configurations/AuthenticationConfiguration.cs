using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfigurations(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options => 
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "api_usuariosexternos";
                });

            return services;
        }
    }
}
