using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Cliente;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _client;

        public ClienteService(HttpClient client, IOptions<ClienteServiceOptions> clienteServiceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(clienteServiceOptions.Value.BaseAddress);
        }

        public void AddToken(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<ClienteResponseMessage> Atualizar(ClienteDto cliente)
        {
            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/clientes/novo", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                var errors = JsonSerializer.Deserialize<List<string>>(result);

                return new ClienteResponseMessage(false, response.StatusCode, errors);
            }

            return new ClienteResponseMessage();
        }

        public async Task<ClienteResponseMessage> Desativar(Guid id)
        {
            var response = await _client.DeleteAsync("/api/clientes/desativar/" + id);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                var errors = JsonSerializer.Deserialize<List<string>>(result);

                return new ClienteResponseMessage(false, response.StatusCode, errors);
            }

            return new ClienteResponseMessage();
        }
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
