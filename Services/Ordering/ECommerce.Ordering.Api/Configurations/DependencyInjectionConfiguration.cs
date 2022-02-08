using ECommerce.Core.Models.Ordering;
using ECommerce.Ordering.Application.Commands;
using ECommerce.Ordering.Application.Handlers.Commands;
using ECommerce.Ordering.Application.Handlers.Queries;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using ECommerce.Ordering.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Ordering.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<CreateOrderCommand, ValidationResult>, CreateOrderCommandHandler>();

            services.AddScoped<IRequestHandler<GetOrderQuery, Order>, GetOrderQueryHandler>();

            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
