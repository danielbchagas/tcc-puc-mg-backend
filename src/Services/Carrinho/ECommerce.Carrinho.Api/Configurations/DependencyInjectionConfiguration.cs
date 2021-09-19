using ECommerce.Carrinho.Api.Extensions;
using ECommerce.Carrinho.Api.Interfaces;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Carrinho.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
        }
    }
}
