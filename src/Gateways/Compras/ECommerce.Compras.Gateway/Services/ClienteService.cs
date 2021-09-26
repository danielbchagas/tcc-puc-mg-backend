using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Cliente;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        public ClienteService(HttpClient client, IOptions<ServiceOptions> serviceOptions) : base(client, serviceOptions)
        {
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
    }
}
