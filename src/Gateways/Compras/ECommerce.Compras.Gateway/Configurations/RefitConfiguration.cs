using ECommerce.Compras.Gateway.Interfaces;
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

namespace ECommerce.Compras.Gateway.Api.Configurations
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ValidateHeaderHandler>();

            services.AddRefitClient<IClienteService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ServiceOptions").GetSection("ClienteUrl").Value))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<IPedidoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ServiceOptions").GetSection("PedidoUrl").Value))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<ICarrinhoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ServiceOptions").GetSection("CarrinhoUrl").Value))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddRefitClient<ICatalogoService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ServiceOptions").GetSection("CatalogoUrl").Value))
                .AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddTransientHttpErrorPolicy(config => config.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            return services;
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
