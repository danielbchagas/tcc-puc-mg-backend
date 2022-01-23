using ECommerce.Gateway.Api.Interfaces;
using ECommerce.Gateway.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static void AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("ServiceOptions").Get<ServiceOption>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ValidateHeaderHandler>();

            services.AddRefitClient<IClienteService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.ClienteUrl))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<IPedidoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.PedidoUrl))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<ICarrinhoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.CarrinhoUrl))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<ICatalogoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.CatalogoUrl))
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

        public class ValidateHeaderHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (!request.Headers.Contains("Authorization"))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Bearer Token não encontrado.")
                    };
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
