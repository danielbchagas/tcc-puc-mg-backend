using ECommerce.Basket.Api.Services.gRPC;
using ECommerce.Basket.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using ECommerce.Ordering.Gateway.Services.gRPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Api.Protos;

namespace ECommerce.Ordering.Gateway.Configurations
{
    public static class GrpcConfiguration
    {
        public static void AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var servicesOptions = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddSingleton<GrpcServicesInterceptor>();

            services.AddScoped<IBasketGrpcClient, BasketGrpcClient>();
            services.AddScoped<ICatalogGrpcClient, CatalogGrpcClient>();
            services.AddScoped<IOrderingGrpcClient, OrderingGrpcClient>();

            services.AddGrpcClient<ShoppingBasketService.ShoppingBasketServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.BasketServiceUrl);
            })
            .AddInterceptor<GrpcServicesInterceptor>();

            services.AddGrpcClient<CatalogService.CatalogServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.CatalogServiceUrl);
            })
            .AddInterceptor<GrpcServicesInterceptor>();

            services.AddGrpcClient<OrderingService.OrderingServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.OrderingServiceUrl);
            })
            .AddInterceptor<GrpcServicesInterceptor>();
        }
    }
}
