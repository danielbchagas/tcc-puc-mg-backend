using ECommerce.Identidade.Api.Interfaces;
using ECommerce.Identidade.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Identidade.Api.Services
{
    public class ClienteService : IClienteService
    {
        private HttpClient Client { get; }

        public ClienteService(HttpClient client, IOptions<HttpOptions> options)
        {
            Client = client;
            Client.BaseAddress = new Uri(options.Value.BaseAddress);
        }

        public void AddToken(string token)
        {
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<ClienteResponseMessage> Novo(ClienteDto cliente)
        {
            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/api/clientes/novo", content);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Erro na requisição para a api de clientes.");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return new ClienteResponseMessage(false, (int)response.StatusCode, JsonSerializer.Deserialize<List<string>>(result));
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new ClienteResponseMessage(false, (int)response.StatusCode, new List<string>() { "Erro no servidor." });
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
