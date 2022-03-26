using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Handlers.Commands;
using ECommerce.Basket.Application.Handlers.Queries;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using ECommerce.Basket.Infrastructure.Repositories;
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
            
            services.AddScoped<IRequestHandler<CreateShoppingBasketCommand, ValidationResult>, CreateShoppingBasketCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteShoppingBasketCommand, ValidationResult>, DeleteShoppingBasketCommandHandler>();
            services.AddScoped<IRequestHandler<GetShoppingBasketByCustomerQuery, ShoppingBasket>, GetShoppingBasketByCustomerQueryHandler>();
            services.AddScoped<IRequestHandler<GetShoppingBasketQuery, ShoppingBasket>, GetShoppingBasketQueryHandler>();
            
            services.AddScoped<IRequestHandler<UpdateBasketItemCommand, ValidationResult>, UpdateBasketItemCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBasketItemCommand, ValidationResult>, DeleteBasketItemCommandHandler>();
            services.AddScoped<IRequestHandler<GetBasketItemQuery, BasketItem>, GetBasketItemQueryHandler>();
            services.AddScoped<IRequestHandler<GetBasketItemByProductQuery, BasketItem>, GetBasketItemByProductQueryHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IShoppingBasketRepository, ShoppingBasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        }
    }
}
