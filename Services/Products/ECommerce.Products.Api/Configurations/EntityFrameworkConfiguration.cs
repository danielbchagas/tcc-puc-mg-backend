﻿using ECommerce.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerce.Products.Api.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.EnableRetryOnFailure();
                })
                .LogTo(Console.WriteLine);
            });
        }
    }
}
