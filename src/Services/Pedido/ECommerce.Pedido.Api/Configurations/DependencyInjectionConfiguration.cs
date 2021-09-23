using ECommerce.Pedido.Domain.Interfaces.Repositories;
using ECommerce.Pedido.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Pedido.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Injeção de dependência
            // Mediator
            services.AddMediatR(typeof(Startup));

            // Mediator - Comandos

            // Mediator - Queries

            // Mediator - Notificações

            // Repositórios
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            #endregion
        }
    }
}
