using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Pedido;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class PedidoService : BaseService, IPedidoService
    {
        public PedidoService(HttpClient client, IOptions<ServiceOptions> serviceOptions) : base(client)
        {
            _client.BaseAddress = new Uri(serviceOptions.Value.PedidoUrl);
        }

        public async Task<ServiceResponse> Adicionar(PedidoDto pedido)
        {
            var json = JsonSerializer.Serialize(pedido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/pedidos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<PedidoDto> Buscar(Guid id)
        {
            var response = await _client.GetAsync($"/api/pedidos/{id}");

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PedidoDto>(content, GetOptions());
        }
    }
}
