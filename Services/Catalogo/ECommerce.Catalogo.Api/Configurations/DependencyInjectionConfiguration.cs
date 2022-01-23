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
            
            #region Mediator
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<GetProdutoQuery, Produto>, GetProdutoQueryHandler>();
            services.AddScoped<IRequestHandler<FilterProdutosQuery, IEnumerable<Produto>>, FilterProdutosQueryHandler>();
            services.AddScoped<IRequestHandler<GetProdutosQuery, IEnumerable<Produto>>, GetProdutosQueryHandler>();
            #endregion

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
