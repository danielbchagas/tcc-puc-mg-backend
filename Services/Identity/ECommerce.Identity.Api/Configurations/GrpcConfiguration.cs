using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models;
using ECommerce.Identity.Api.Services.gRPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerce.Identity.Api.Configurations
{
    public static class GrpcConfiguration
    {
        public static void AddGrpcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var servicesOptions = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddScoped<ICustomerGrpcClient, CustomerGrpcClient>();

            services.AddGrpcClient<Customer.Api.Protos.CustomerService.CustomerServiceClient>(options =>
            {
                options.Address = new Uri(servicesOptions.CustomerServiceUrl);
            });
        }
    }
}
