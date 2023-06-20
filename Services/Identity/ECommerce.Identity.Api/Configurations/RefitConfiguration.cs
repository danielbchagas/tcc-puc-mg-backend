using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Options;
using ECommerce.Identity.Api.Services.REST;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace ECommerce.Identity.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var viaCepOptions = configuration.GetSection("ViaCepOptions").Get<ViaCepOption>();

            services.AddScoped<IViaCepService, ViaCepService>();

            services.AddRefitClient<IViaCepRequest>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(viaCepOptions.Url));

            return services;
        }
    }
}
