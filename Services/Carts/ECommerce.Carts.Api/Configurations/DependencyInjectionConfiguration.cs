using ECommerce.Carts.Application.Commands;
using ECommerce.Carts.Application.Handlers.Commands;
using ECommerce.Carts.Application.Handlers.Queries;
using ECommerce.Carts.Application.Queries;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Carts.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Mediator
            services.AddMediatR(typeof(Startup));
            
            services.AddScoped<IRequestHandler<CreateCartCommand, ValidationResult>, CreateCartCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCartCommand, ValidationResult>, DeleteCartCommandHandler>();
            services.AddScoped<IRequestHandler<GetCartByCustomerQuery, Domain.Models.Cart>, GetCartByCustomerQueryHandler>();
            services.AddScoped<IRequestHandler<GetCartQuery, Domain.Models.Cart>, GetCartQueryHandler>();
            
            services.AddScoped<IRequestHandler<CreateItemCommand, ValidationResult>, CreateItemCommanHandler>();
            services.AddScoped<IRequestHandler<DeleteItemCommand, ValidationResult>, DeleteItemCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
        }
    }
}
