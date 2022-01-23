using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Application.Handlers.Commands;
using ECommerce.Carrinho.Application.Handlers.Queries;
using ECommerce.Carrinho.Application.Queries;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Carrinho.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Mediator
            services.AddMediatR(typeof(Startup));

            // Carrinho
            services.AddScoped<IRequestHandler<CreateCarrinhoCommand, ValidationResult>, CreateCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCarrinhoCommand, ValidationResult>, DeleteCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<GetCarrinhoByClienteQuery, Domain.Models.CarrinhoCompras>, GetCarrinhoByClienteQueryHandler>();
            services.AddScoped<IRequestHandler<GetCarrinhoQuery, Domain.Models.CarrinhoCompras>, GetCarrinhoQueryHandler>();

            // Item carrinho
            services.AddScoped<IRequestHandler<CreateItemCarrinhoCommand, ValidationResult>, CreateItemCarrinhoCommanHandler>();
            services.AddScoped<IRequestHandler<DeleteItemCarrinhoCommand, ValidationResult>, DeleteItemCarrinhoCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
        }
    }
}
