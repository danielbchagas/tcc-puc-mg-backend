using ECommerce.Carrinho.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Carrinho.Api.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void AddEntityFrameworkConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
