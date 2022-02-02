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
    public class BasketItemController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketItemController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItemDto item)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _catalogService.Get(item.ProductId);

            #region Is product available in stock?
            if (response.Content.Quantity < item.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Stock update
            response.Content.Quantity -= item.Quantity;
            var result = await _catalogService.Update(response.Content, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            #region Basket update
            item.Name = response.Content.Name;
            item.Image = response.Content.Image;
            item.Value = response.Content.Value;

            result = await _basketService.CreateBasketItem(item, accessToken);

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
            var response = await _basketService.DeleteBasketItem(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
