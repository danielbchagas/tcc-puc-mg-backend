using ECommerce.Pedido.Application.Commands;
using ECommerce.Pedido.Application.Handlers.Commands;
using ECommerce.Pedido.Domain.Interfaces.Repositories;
using ECommerce.Pedido.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Pedido.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(typeof(Startup));

            // Mediator - Comandos
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, AdicionarPedidoCommandHandler>();

            // Mediator - Queries

            // Mediator - Notificações

            // Repositórios
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }
    }
}
