using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Handlers.Commands;
using ECommerce.Basket.Application.Handlers.Queries;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
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
            
            services.AddScoped<IRequestHandler<CreateBasketCommand, (ValidationResult, Domain.Models.Basket) >, CreateBasketCommandHandler>();
            services.AddScoped<IRequestHandler<DisableBasketCommand, ValidationResult>, DisableBasketCommandHandler>();
            services.AddScoped<IRequestHandler<GetBasketByCustomerQuery, Domain.Models.Basket>, GetBasketByCustomerQueryHandler>();
            services.AddScoped<IRequestHandler<UpdateBasketCommand, (ValidationResult, Domain.Models.Basket) >, UpdateBasketCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
