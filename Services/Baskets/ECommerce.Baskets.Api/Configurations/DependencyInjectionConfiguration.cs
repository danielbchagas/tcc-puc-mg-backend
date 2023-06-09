using ECommerce.Baskets.Api.Services;
using ECommerce.Baskets.Application.Commands.Basket;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Application.Handlers.Commands.Basket;
using ECommerce.Baskets.Application.Handlers.Commands.Item;
using ECommerce.Baskets.Application.Handlers.Queries;
using ECommerce.Baskets.Application.Queries;
using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using ECommerce.Baskets.Infrastructure.Data;
using ECommerce.Baskets.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Baskets.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Mediator
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<CreateBasketCommand, (ValidationResult, Domain.Models.Basket)>, CreateBasketCommandHandler>();
            services.AddScoped<IRequestHandler<DisableBasketCommand, ValidationResult>, DisableBasketCommandHandler>();
            services.AddScoped<IRequestHandler<GetBasketByIdQuery, Domain.Models.Basket>, GetBasketByIdQueryHandler>();

            services.AddScoped<IRequestHandler<IncludeItemCommand, (ValidationResult, Domain.Models.Basket)>, IncludeItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemCommand, (ValidationResult, Domain.Models.Basket)>, RemoveItemCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHostedService<CloseAbandonedBasketService>();
        }
    }
}
