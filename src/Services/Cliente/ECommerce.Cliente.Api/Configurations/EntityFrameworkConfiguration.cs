using ECommerce.Cliente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Cliente.Api.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseSqlite(connectionString);
            });
        }
    }
}
