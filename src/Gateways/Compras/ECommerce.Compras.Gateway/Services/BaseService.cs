using ECommerce.Compras.Gateway.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerce.Compras.Gateway.Services
{
    public class BaseService
    {
        protected readonly HttpClient _client;

        public BaseService(HttpClient client, IOptions<ServiceOptions> serviceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(serviceOptions.Value.PedidoUrl);
        }

        protected JsonSerializerOptions GetOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return options;
        }
    }
}
