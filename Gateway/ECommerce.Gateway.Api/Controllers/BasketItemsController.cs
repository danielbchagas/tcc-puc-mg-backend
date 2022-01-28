using ECommerce.Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Threading.Tasks;
using ECommerce.Gateway.Api.Models;

namespace ECommerce.Gateway.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;
        private readonly IBasketService _basketService;

        public BasketItemsController(ICatalogoService catalogoService, IBasketService basketService)
        {
            _catalogoService = catalogoService;
            _basketService = basketService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItem item)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _catalogoService.Get(item.ProductId);

            #region Validação de item disponível em estoque
            if (response.Content.QuantidadeEstoque < item.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Atualização do estoque
            response.Content.QuantidadeEstoque -= item.Quantity;
            var result = await _catalogoService.Update(response.Content, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            #region Atualização de carrinho
            item.Name = response.Content.Nome;
            item.Image = response.Content.Imagem;
            item.Value = response.Content.Valor;

            result = await _basketService.CreateItemCarrinho(item, accessToken);

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
            var response = await _basketService.DeleteItemCarrinho(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
