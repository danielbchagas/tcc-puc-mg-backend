using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Carrinho;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class CarrinhoService : BaseService, ICarrinhoService
    {
        public CarrinhoService(HttpClient client, IOptions<ServiceOptions> serviceOptions) : base(client)
        {
            _client.BaseAddress = new Uri(serviceOptions.Value.CarrinhoUrl);
        }

        #region Carrinho
        public async Task<CarrinhoDto> BuscarCarrinho(Guid id)
        {
            var response = await _client.GetAsync($"/api/carrinhos/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            return JsonSerializer.Deserialize<CarrinhoDto>(await response.Content.ReadAsStringAsync(), GetOptions());
        }

        public async Task<ServiceResponse> AdicionarCarrinho(CarrinhoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/carrinhos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> ExcluirCarrinho(Guid id)
        {
            var response = await _client.DeleteAsync($"/api/carrinhos/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }
        #endregion

        #region Item carrinho
        public async Task<ServiceResponse> AdicionarItemCarrinho(ItemCarrinhoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/itenscarrinhos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ServiceResponse> ExcluirItemCarrinho(Guid id)
        {
            var response = await _client.DeleteAsync($"/api/itenscarrinhos/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }
        #endregion
    }
}
