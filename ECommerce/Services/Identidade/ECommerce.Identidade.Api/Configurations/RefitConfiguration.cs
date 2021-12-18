using ECommerce.Identidade.Api.Interfaces;
using ECommerce.Identidade.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Net;
using System.Net.Http;

namespace ECommerce.Identidade.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static void AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("ServiceOptions").Get<ServiceOptions>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRefitClient<IClienteService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.ClienteUrl))
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
