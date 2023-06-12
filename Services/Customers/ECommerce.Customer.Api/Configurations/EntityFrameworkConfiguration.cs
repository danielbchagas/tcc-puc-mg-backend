using ECommerce.Customers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerce.Customer.Api.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(connectionString, opt =>
                {
                    opt.EnableRetryOnFailure();
                })
                .LogTo(Console.WriteLine);
            });
        }
    }
}
