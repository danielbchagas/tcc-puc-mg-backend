using System.Collections.Generic;
using ECommerce.Catalog.Application.Handlers.Queries;
using ECommerce.Catalog.Application.Queries;
using ECommerce.Catalog.Domain.Interfaces.Repositories;
using ECommerce.Catalog.Domain.Models;
using ECommerce.Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Catalog.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            #region Mediator
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<GetProductQuery, Product>, GetProductQueryHandler>();
            services.AddScoped<IRequestHandler<FilterProductsQuery, IEnumerable<Product>>, FilterProductsQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductsQuery, IEnumerable<Product>>, GetProductsQueryHandler>();
            #endregion
            
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
