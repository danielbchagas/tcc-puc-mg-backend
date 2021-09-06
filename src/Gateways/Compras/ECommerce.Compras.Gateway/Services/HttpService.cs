using ECommerce.Compras.Gateway.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient HttpClient { get; }
        private HttpOptions HttpOptions { get; }

        public HttpService(IOptions<HttpOptions> httpOptions)
        {
            HttpOptions = httpOptions.Value;

            HttpClient.BaseAddress = new Uri(HttpOptions.BaseAddressCarrinho);
        }
    }

    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-API-KEY"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("You must supply an API key header called X-API-KEY")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
