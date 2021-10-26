using ECommerce.Identidade.Api.Interfaces;
using ECommerce.Identidade.Api.Models;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System;
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
        private readonly HttpClient _client;

        public ClienteService(HttpClient client, IOptions<ClienteServiceOptions> options)
        {
            _client = client;
            _client.BaseAddress = new Uri(options.Value.BaseAddress);
        }

        public void AddToken(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<ValidationResult> Adicionar(ClienteDto cliente)
        {
            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/clientes", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> Adicionar(DocumentoDto documento)
        {
            var json = JsonSerializer.Serialize(documento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/documentos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> Adicionar(TelefoneDto telefone)
        {
            var json = JsonSerializer.Serialize(telefone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/telefones", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> Adicionar(EmailDto email)
        {
            var json = JsonSerializer.Serialize(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/emails", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
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
