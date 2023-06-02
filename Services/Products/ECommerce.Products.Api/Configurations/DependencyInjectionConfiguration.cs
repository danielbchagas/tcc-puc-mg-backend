using System.Collections.Generic;
using ECommerce.Products.Api;
using ECommerce.Products.Application.Commands;
using ECommerce.Products.Application.Handlers.Commands;
using ECommerce.Products.Application.Handlers.Queries;
using ECommerce.Products.Application.Queries;
using ECommerce.Products.Domain.Interfaces.Repositories;
using ECommerce.Products.Domain.Models;
using ECommerce.Products.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Products.Api.Configurations
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
