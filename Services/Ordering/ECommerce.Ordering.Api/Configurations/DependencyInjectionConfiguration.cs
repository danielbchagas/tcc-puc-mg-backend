using ECommerce.Ordering.Application.Commands;
using ECommerce.Ordering.Application.Handlers.Commands;
using ECommerce.Ordering.Application.Handlers.Queries;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

namespace ECommerce.Ordering.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(typeof(Startup));

            // Mediator - Comandos
            services.AddScoped<IRequestHandler<CreateOrderCommand, ValidationResult>, CreateOrderCommandHandler>();

            // Mediator - Queries
            services.AddScoped<IRequestHandler<GetOrderQuery, PedidoCliente>, GetOrderQueryHandler>();

            // Mediator - Notificações

            // Repositórios
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
