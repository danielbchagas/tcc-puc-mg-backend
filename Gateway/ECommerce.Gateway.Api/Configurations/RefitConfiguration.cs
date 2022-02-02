using ECommerce.Gateway.Api.Interfaces;
using ECommerce.Gateway.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Net.Http;

namespace ECommerce.Gateway.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static void AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddRefitClient<ICustomerService>()
                .ConfigureHttpClient(config =>
                {
                    config.BaseAddress = new Uri(options.CustomerServiceUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<IOrderingService>()
                .ConfigureHttpClient(config =>
                {
                    config.BaseAddress = new Uri(options.CustomerServiceUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<IBasketService>()
                .ConfigureHttpClient(config =>
                {
                    config.BaseAddress = new Uri(options.CustomerServiceUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddRefitClient<ICatalogService>()
                .ConfigureHttpClient(config =>
                {
                    config.BaseAddress = new Uri(options.CustomerServiceUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
