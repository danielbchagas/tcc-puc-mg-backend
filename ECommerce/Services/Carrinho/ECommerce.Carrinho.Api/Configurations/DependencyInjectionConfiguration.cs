using ECommerce.Carrinho.Api.Extensions;
using ECommerce.Carrinho.Api.Interfaces;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ECommerce.Carrinho.Application.Commands;
using FluentValidation.Results;
using ECommerce.Carrinho.Application.Handlers.Commands;
using ECommerce.Carrinho.Application.Handlers.Queries;
using ECommerce.Carrinho.Application.Queries;

namespace ECommerce.Carrinho.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Mediator
            services.AddMediatR(typeof(Startup));

            // Carrinho
            services.AddScoped<IRequestHandler<AdicionarCarrinhoCommand, ValidationResult>, AdicionarCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirCarrinhoCommand, ValidationResult>, ExcluirCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<BuscarCarrinhoPorClienteQuery, Domain.Models.Carrinho>, BuscarCarrinhoPorClienteQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarCarrinhoQuery, Domain.Models.Carrinho>, BuscarCarrinhoQueryHandler>();

            // Item carrinho
            services.AddScoped<IRequestHandler<AdicionarItemCarrinhoCommand, ValidationResult>, AdicionarItemCarrinhoCommanHandler>();
            services.AddScoped<IRequestHandler<ExcluirItemCarrinhoCommand, ValidationResult>, ExcluirItemCarrinhoCommandHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
        }
    }
}
