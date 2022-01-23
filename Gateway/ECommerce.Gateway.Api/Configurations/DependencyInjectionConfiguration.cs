﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Gateway.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }
    }
}