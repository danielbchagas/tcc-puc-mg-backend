using ECommerce.Catalogo.Api.Extensions;
using ECommerce.Catalogo.Api.Interfaces;
using ECommerce.Catalogo.Application.Handlers.Queries;
using ECommerce.Catalogo.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Catalogo.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region Mediator
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<BuscarProdutoPorIdQuery, Produto>, BuscarProdutoPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosFiltradosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosPaginadosQueryHandler>();
            #endregion

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
