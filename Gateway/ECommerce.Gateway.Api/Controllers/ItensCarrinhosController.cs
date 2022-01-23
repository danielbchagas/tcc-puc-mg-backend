using ECommerce.Gateway.Api.Interfaces;
using ECommerce.Gateway.Api.Models.Carrinho;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhosController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICarrinhoService _carrinhoService;

        public ItensCarrinhosController(ICatalogoService catalogoService, ICarrinhoService carrinhoService)
        {
            _catalogoService = catalogoService;
            _carrinhoService = carrinhoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(ItemCarrinhoDto item)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _catalogoService.Get(item.ProdutoId);

            #region Validação de item disponível em estoque
            if (response.Content.QuantidadeEstoque < item.Quantidade)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Atualização do estoque
            response.Content.QuantidadeEstoque -= item.Quantidade;
            var result = await _catalogoService.Update(response.Content, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            #region Atualização de carrinho
            item.Nome = response.Content.Nome;
            item.Imagem = response.Content.Imagem;
            item.Valor = response.Content.Valor;

            result = await _carrinhoService.CreateItemCarrinho(item, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _carrinhoService.DeleteItemCarrinho(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error.Content);

            return NoContent();
        }
    }
}
