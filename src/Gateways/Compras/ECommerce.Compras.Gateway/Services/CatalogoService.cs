using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using ECommerce.Compras.Gateway.Models.Catalogo;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly HttpClient _client;

        public CatalogoService(HttpClient client, IOptions<ServiceOptions> clienteServiceOptions)
        {
            _client = client;
            _client.BaseAddress = new Uri(clienteServiceOptions.Value.CatalogoUrl);
        }

        public async Task<ValidationResult> Atualizar(AtualizarProdutoDto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/produtos/atualizar", content);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<ValidationResult>(result);
            }

            return new ValidationResult();
        }

        public async Task<BuscarProdutoDto> Buscar(Guid id)
        {
            var response = await _client.GetAsync($"/api/produtos/buscar/{id}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<BuscarProdutoDto>(content, GetOptions());
        }

        public async Task<IEnumerable<BuscarProdutoDto>> Buscar(int pagina, int linhas)
        {
            var response = await _client.GetAsync($"/api/produtos/buscar/{pagina}/{linhas}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<BuscarProdutoDto>>(content, GetOptions());
        }

        public async Task<IEnumerable<BuscarProdutoDto>> Buscar(string nome, int pagina, int linhas)
        {
            var response = await _client.GetAsync($"/api/produtos/buscar/{nome}/{pagina}/{linhas}");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<BuscarProdutoDto>>(content, GetOptions());
        }

        public async Task<ValidationResult> Cadastrar(CadastrarProdutoDto produto)
        {
            var json = JsonSerializer.Serialize(produto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/produtos/cadastrar", content);

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
