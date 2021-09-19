using ECommerce.Catalogo.Application.Handlers.Queries;
using ECommerce.Catalogo.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Catalogo.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Injeção de dependência
            // Mediator
            services.AddMediatR(typeof(Startup));

            // Mediator - Queries
            services.AddScoped<IRequestHandler<BuscarProdutoPorIdQuery, Produto>, BuscarProdutoPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosFiltradosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosPaginadosQueryHandler>();
            
            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion
        }
    }
}
