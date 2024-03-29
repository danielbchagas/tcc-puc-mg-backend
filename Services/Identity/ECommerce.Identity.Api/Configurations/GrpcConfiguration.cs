﻿using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Options;
using ECommerce.Identity.Api.Services.gRPC;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ECommerce.Identity.Api.Configurations
{
    public static class GrpcConfiguration
    {
        public static void AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();

            var servicesOptions = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddScoped<ICustomerGrpcClient, CustomerGrpcService>();

            services.AddGrpcClient<Customers.Api.Protos.CustomerService.CustomerServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.CustomerServiceUrl);
            }).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));
        }
    }
}
