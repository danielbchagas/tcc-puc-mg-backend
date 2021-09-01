using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Carrinho.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
        }
    }
}
