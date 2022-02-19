using ECommerce.Basket.Domain.Models;
using ECommerce.Catalog.Domain.Models;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketItemController(IBasketService basketService, 
            ICatalogService catalogService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItemDto item)
        {
            var accessToken = await GetToken();

            #region Stock update
            var product = await GetProduct(item.Id);
            var basketItem = await GetBasketItem(item.Id);

            if (product == null)
                return NotFound();

            if (product.Quantity < item.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");

            product = UpdateProductQuantity(basketItem, item, product);

            var createBasketItemResult = await _catalogService.Update(item.Id, product, accessToken);

            if (!createBasketItemResult.IsSuccessStatusCode)
                return BadRequest(createBasketItemResult.Error);
            #endregion

            #region Basket update
            var newBasketItem = new BasketItem(Guid.NewGuid(), product.Name, item.Quantity, product.Value, product.Image, productId: product.Id, item.CustomerBasketId);
            
            createBasketItemResult = await _basketService.CreateBasketItem(newBasketItem, accessToken);

            if (!createBasketItemResult.IsSuccessStatusCode)
            {
                await ReturnProductsToStock(item, product);
                return BadRequest(createBasketItemResult.Error);
            }

            return Ok();
            #endregion
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = await GetToken();

            #region Catalog update
            var basketItem = await _basketService.GetBasketItem(id, accessToken);

            var product = await GetProduct(basketItem.Content.Id);

            if (product == null)
                return NotFound();

            product.Quantity += basketItem.Content.Quantity;
            var result = await _catalogService.Update(basketItem.Content.Id, product, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            #region Delete Basket Item
            var deleteBasketItemResponse = await _basketService.DeleteBasketItem(id, accessToken);

            if (!deleteBasketItemResponse.IsSuccessStatusCode)
                return BadRequest(deleteBasketItemResponse.Error);

            return NoContent();
            #endregion
        }

        #region Helpers
        protected async Task<string> GetToken()
            => await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

        private async Task<Product> GetProduct(Guid id)
        {
            var response = await _catalogService.Get(id);
            return response.Content;
        }

        private async Task<BasketItem> GetBasketItem(Guid id)
        {
            var accessToken = await GetToken();
            var response = await _basketService.GetBasketItem(id, accessToken);
            return response.Content;
        }

        private async Task ReturnProductsToStock(BasketItemDto item, Product product)
        {
            var accessToken = await GetToken();

            product.Quantity += item.Quantity;
            await _catalogService.Update(item.Id, product, accessToken);
        }

        private Product UpdateProductQuantity(BasketItem basketItem, BasketItemDto item, Product product)
        {
            if (basketItem == null)
                product.Quantity -= item.Quantity;

            if (basketItem.Quantity > item.Quantity)
                product.Quantity += (basketItem.Quantity - item.Quantity);
            else
                product.Quantity -= Math.Abs(basketItem.Quantity - item.Quantity);

            return product;
        }
        #endregion
    }
}
