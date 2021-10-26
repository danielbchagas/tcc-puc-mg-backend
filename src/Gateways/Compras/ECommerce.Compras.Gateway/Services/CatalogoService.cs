using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Catalogo;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class CatalogoService : BaseService, ICatalogoService
    {
        public CatalogoService(HttpClient client, IOptions<ServiceOptions> serviceOptions) : base(client)
        {
            _client.BaseAddress = new Uri(serviceOptions.Value.CatalogoUrl);
        }

        public async Task<ServiceResponse> Atualizar(ProdutoDto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/produtos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }

        public async Task<ProdutoDto> Buscar(Guid id)
        {
            var response = await _client.GetAsync($"/api/produtos/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProdutoDto>(content, GetOptions());
        }

        public async Task<IEnumerable<ProdutoDto>> Buscar(int pagina, int linhas)
        {
            var response = await _client.GetAsync($"/api/produtos/{pagina}/{linhas}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<ProdutoDto>>(content, GetOptions());
        }

        public async Task<IEnumerable<ProdutoDto>> Buscar(string nome, int pagina, int linhas)
        {
            var response = await _client.GetAsync($"/api/produtos/{nome}/{pagina}/{linhas}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<ProdutoDto>>(content, GetOptions());
        }

        public async Task<ServiceResponse> Cadastrar(ProdutoDto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/produtos", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ServiceResponse>(result, GetOptions());
            }

            return new ServiceResponse();
        }
    }
}
