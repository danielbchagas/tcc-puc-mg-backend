using System;
using System.Net;
using System.Net.Http;
using ECommerce.Identity.Api.Interfaces;
using ECommerce.Identity.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace ECommerce.Identity.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static void AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRefitClient<ICustomerService>()
                .ConfigureHttpClient(config => 
                {
                    var options = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

                    config.BaseAddress = new Uri(options.CustomerServiceUrl);
                })
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
