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

            // Commands
            // Carrinho
            services.AddScoped<IRequestHandler<AdicionarCarrinhoCommand, ValidationResult>, AdicionarCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarCarrinhoCommand, ValidationResult>, AtualizarCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirCarrinhoPorProdutoIdCommand, ValidationResult>, ExcluirCarrinhoPorProdutoIdCommandHandler>();

            // Item carrinho
            services.AddScoped<IRequestHandler<AdicionarItemCarrinhoCommand, ValidationResult>, AdicionarItemCarrinhoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemCarrinhoCommand, ValidationResult>, AtualizarItemCarrinhoCommanHandler>();
            services.AddScoped<IRequestHandler<ExcluirItemCarrinhoPorProdutoIdCommand, ValidationResult>, ExcluirItemCarrinhoPorProdutoIdCommandHandler>();
            
            // Queries
            // Carrinho
            services.AddScoped<IRequestHandler<BuscarPorClienteIdQuery, Domain.Models.Carrinho>, BuscarPorClienteIdQueryHandler>();

            // Item carrinho
            services.AddScoped<IRequestHandler<BuscarPorProdutoIdQuery, Domain.Models.ItemCarrinho>, BuscarPorProdutoIdQueryHandler>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoRepository>();
        }
    }
}
