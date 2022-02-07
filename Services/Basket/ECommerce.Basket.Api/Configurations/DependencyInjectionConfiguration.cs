using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Handlers.Commands;
using ECommerce.Basket.Application.Handlers.Queries;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Infrastructure.Repositories;
using ECommerce.Core.Models.Basket;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Basket.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Mediator
            services.AddMediatR(typeof(Startup));
            
            services.AddScoped<IRequestHandler<CreateCustomerBasketCommand, ValidationResult>, CreateCustomerBasketCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCustomerBasketCommand, ValidationResult>, DeleteCustomerBasketCommandHandler>();
            services.AddScoped<IRequestHandler<GetCustomerBasketByCustomerQuery, CustomerBasket>, GetCustomerBasketByCustomerQueryHandler>();
            services.AddScoped<IRequestHandler<GetCustomerBasketQuery, CustomerBasket>, GetCustomerBasketQueryHandler>();
            
            services.AddScoped<IRequestHandler<CreateBasketItemCommand, ValidationResult>, CreateBasketItemCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBasketItemCommand, ValidationResult>, DeleteBasketItemCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<ICustomerBasketRepository, CustomerBasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        }
    }
}
