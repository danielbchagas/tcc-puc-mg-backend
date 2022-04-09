using ECommerce.Basket.Api.Protos;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using ECommerce.Ordering.Gateway.Services.gRPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using ECommerce.Catalog.Api.Protos;
using ECommerce.Ordering.Api.Protos;
using ECommerce.Customer.Api.Protos;
using Grpc.Net.Client.Web;
using System.Net.Http;

namespace ECommerce.Ordering.Gateway.Configurations
{
    public static class GrpcConfiguration
    {
        public static void AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();

            var servicesOptions = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddSingleton<GrpcServicesInterceptor>();

            services.AddScoped<IBasketGrpcClient, BasketGrpcClient>();
            services.AddScoped<ICatalogGrpcClient, CatalogGrpcClient>();
            services.AddScoped<IOrderingGrpcClient, OrderingGrpcClient>();
            services.AddScoped<ICustomerGrpcClient, CustomerGrpcClient>();

            services.AddGrpcClient<ShoppingBasketService.ShoppingBasketServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.BasketServiceUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()))
            .AddInterceptor<GrpcServicesInterceptor>();

            services.AddGrpcClient<CatalogService.CatalogServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.CatalogServiceUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()))
            .AddInterceptor<GrpcServicesInterceptor>();

            services.AddGrpcClient<OrderingService.OrderingServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.OrderingServiceUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()))
            .AddInterceptor<GrpcServicesInterceptor>();

            services.AddGrpcClient<CustomerService.CustomerServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.CustomerServiceUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()))
            .AddInterceptor<GrpcServicesInterceptor>();
        }
    }
}
