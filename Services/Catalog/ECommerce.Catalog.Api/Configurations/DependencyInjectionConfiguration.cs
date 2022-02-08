using ECommerce.Catalog.Application.Commands;
using ECommerce.Catalog.Application.Handlers.Commands;
using ECommerce.Catalog.Application.Handlers.Queries;
using ECommerce.Catalog.Application.Queries;
using ECommerce.Catalog.Domain.Interfaces.Repositories;
using ECommerce.Catalog.Infrastructure.Repositories;
using ECommerce.Core.Models.Catalog;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Catalog.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IRequestHandler<GetProductQuery, Product>, GetProductQueryHandler>();
            services.AddScoped<IRequestHandler<FilterProductsQuery, IEnumerable<Product>>, FilterProductsQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductsQuery, IEnumerable<Product>>, GetProductsQueryHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, UpdateProductCommandHandler>();

            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
