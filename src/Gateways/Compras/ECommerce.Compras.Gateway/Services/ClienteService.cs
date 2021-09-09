using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Cliente;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System;
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

        public async Task<ValidationResult> Atualizar(AtualizarClienteDto cliente)
        {
            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/clientes/atualizar", content);

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<BuscarClienteDto> Buscar(Guid id)
        {
            var response = await _client.GetAsync($"/api/clientes/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<BuscarClienteDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<ValidationResult> Desativar(Guid id)
        {
            var response = await _client.DeleteAsync($"/api/clientes/desativar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        private JsonSerializerOptions GetOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return options;
        }
    }
}
