﻿using ECommerce.Products.Application.Commands;
using ECommerce.Products.Application.Handlers.Commands;
using ECommerce.Products.Application.Handlers.Queries;
using ECommerce.Products.Application.Queries;
using ECommerce.Products.Domain.Interfaces.Data;
using ECommerce.Products.Domain.Interfaces.Repositories;
using ECommerce.Products.Domain.Models;
using ECommerce.Products.Infrastructure.Data;
using ECommerce.Products.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ECommerce.Products.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMediatR(typeof(Startup));

            #region Commands
            services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, UpdateProductCommandHandler>();
            #endregion

            #region Queries
            services.AddScoped<IRequestHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductsQuery, IEnumerable<Product>>, GetProductsQueryHandler>();
            services.AddScoped<IRequestHandler<GetImageQuery, (Guid, string)>, GetImageQueryHandler>();
            #endregion

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
