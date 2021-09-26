using ECommerce.Compras.Gateway.Dtos.Carrinho;
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

        public CarrinhoService(HttpClient client, IOptions<ServiceOptions> serviceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(serviceOptions.Value.CarrinhoUrl);
        }

        public async Task<Carrinho> Buscar(BuscarCarrinhoPorClienteDto dto)
        {
            var response = await _client.GetAsync($"/api/carrinhos/buscar/{dto}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<Carrinho>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<ServiceResponse> Adicionar(AdicionarCarrinhoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/carrinhos/adicionar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> Excluir(ExcluirCarrinhoDto dto)
        {
            var response = await _client.DeleteAsync($"/api/carrinhos/excluir/{dto}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> Adicionar(AdicionarItemCarrinhoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/itenscarrinhos/adicionar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> Excluir(ExcluirItemCarrinhoDto dto)
        {
            var response = await _client.DeleteAsync($"/api/itenscarrinhos/excluir/{dto}");

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
