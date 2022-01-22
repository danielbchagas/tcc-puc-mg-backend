﻿using ECommerce.Identidade.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Identidade.Api.Configurations
{
    public static class OptionsConfiguration
    {
        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqOption>(config => configuration.GetSection("RabbitMqOptions").Bind(config));
            services.Configure<JwtOption>(config => configuration.GetSection("JwtOptions").Bind(config));
            services.Configure<GoogleOAuthOption>(config => configuration.GetSection("GoogleOauth").Bind(config));
        }
    }
}
