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
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _client;

        public ClienteService(HttpClient client, IOptions<ServiceOptions> clienteServiceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(clienteServiceOptions.Value.ClienteUrl);
        }

        public async Task<ValidationResult> AdicionarDocumento(DocumentoDto documento)
        {
            var json = JsonSerializer.Serialize(documento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/documentos/novo", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AdicionarEmail(EmailDto email)
        {
            var json = JsonSerializer.Serialize(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/emails/novo", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AdicionarEndereco(EnderecoDto endereco)
        {
            var json = JsonSerializer.Serialize(endereco);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/enderecos/novo", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AdicionarTelefone(TelefoneDto telefone)
        {
            var json = JsonSerializer.Serialize(telefone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/telefones/novo", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarCliente(ClienteDto cliente)
        {
            var json = JsonSerializer.Serialize(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/clientes/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarDocumento(DocumentoDto documento)
        {
            var json = JsonSerializer.Serialize(documento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/documentos/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarEmail(EmailDto email)
        {
            var json = JsonSerializer.Serialize(email);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/emails/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarEndereco(EnderecoDto endereco)
        {
            var json = JsonSerializer.Serialize(endereco);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/enderecos/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarTelefone(TelefoneDto telefone)
        {
            var json = JsonSerializer.Serialize(telefone);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/telefones/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<ClienteDto> BuscarCliente(Guid id)
        {
            var response = await _client.GetAsync($"/api/clientes/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<ClienteDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<DocumentoDto> BuscarDocumento(Guid id)
        {
            var response = await _client.GetAsync($"/api/documentos/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<DocumentoDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<EmailDto> BuscarEmail(Guid id)
        {
            var response = await _client.GetAsync($"/api/emails/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<EmailDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<EnderecoDto> BuscarEndereco(Guid id)
        {
            var response = await _client.GetAsync($"/api/enderecos/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<EnderecoDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<TelefoneDto> BuscarTelefone(Guid id)
        {
            var response = await _client.GetAsync($"/api/telefones/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<TelefoneDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<ValidationResult> DesativarCliente(Guid id)
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
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return options;
        }
    }
}
