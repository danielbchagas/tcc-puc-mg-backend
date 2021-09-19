using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly HttpClient _client;

        public CarrinhoService(HttpClient client, IOptions<ServiceOptions> clienteServiceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(clienteServiceOptions.Value.CarrinhoUrl);
        }

        public async Task<Carrinho> Buscar()
        {
            var response = await _client.GetAsync("/api/carrinhos/buscar-carrinho");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<Carrinho>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task Excluir(Guid produtoId)
        {
            await _client.DeleteAsync($"/api/carrinhos/excluir-item/{produtoId}");
        }

        public async Task<ServiceResponse> Atualizar(ItemCarrinho item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/carrinhos/atualizar-carrinho", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
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
