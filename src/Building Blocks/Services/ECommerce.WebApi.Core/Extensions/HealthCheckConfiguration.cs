using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.WebApi.Core.Extensions
{
    public static class HealthCheckConfiguration
    {
        public static void AddHealthCheckConfiguration<TDbContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName = "DefaultConnection") where TDbContext : DbContext
        {
            services.AddHealthChecks()
                .AddDbContextCheck<TDbContext>();

            services.AddDbContext<TDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });
        }
    }
}
