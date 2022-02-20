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
        private readonly IBasketGrpcClient _basketGrpcClient;

        public BasketItemController(IBasketService basketService, 
            ICatalogService catalogService, 
            IHttpContextAccessor httpContextAccessor,
            IBasketGrpcClient basketGrpcClient)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _httpContextAccessor = httpContextAccessor;
            _basketGrpcClient = basketGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItemDto item)
        {
            var accessToken = await GetToken();

            #region Stock update
            var product = await GetProduct(item.ProductId);
            
            var basketItem = await _basketGrpcClient.GetBasketItemByProduct(new Basket.Api.Protos.GetBasketItemByProductRequest
            {
                Produtid = Convert.ToString(item.ProductId)
            });

            // TO-DO: Catalog and basket operation

            if (product == null)
                return NotFound();

            if (product.Quantity < item.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");

            var createBasketItemResult = await _catalogService.Update(item.ProductId, product, accessToken);

            if (!createBasketItemResult.IsSuccessStatusCode)
                return BadRequest(createBasketItemResult.Error);
            #endregion

            #region Basket update
            var _createBasketItemResult = await _basketGrpcClient.AddBasketItem(new Basket.Api.Protos.AddBasketItemRequest
            {
                Id = Convert.ToString(Guid.NewGuid()),
                Name = product.Name,
                Quantity = item.Quantity,
                Image = product.Image,
                Value = Convert.ToDouble(product.Value),
                Productid = Convert.ToString(product.Id),
                Shoppingbasketid = Convert.ToString(item.BasketId),
            });

            if (!_createBasketItemResult.Isvalid)
            {
                return BadRequest(_createBasketItemResult.Message);
            }

            return Ok();
            #endregion
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // TO-DO: Catalog operation
            #region Catalog update
            
            #endregion

            #region Delete Basket Item
            var response = await _basketGrpcClient.RemoveBasketItem(new Basket.Api.Protos.RemoveBasketItemRequest
            {
                Id = Convert.ToString(id)
            });

            if (!response.Isvalid)
                return BadRequest(response.Message);

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

        private Product UpdateProductQuantity(Basket.Api.Protos.GetBasketItemByProductResponse basketItem, BasketItemDto item, Product product)
        {
            if (basketItem == null)
            {
                product.Quantity -= item.Quantity;
                return product;
            }

            if (basketItem.Item.Quantity > item.Quantity)
                product.Quantity += (basketItem.Item.Quantity - item.Quantity);
            else
                product.Quantity -= Math.Abs(basketItem.Item.Quantity - item.Quantity);

            return product;
        }
        #endregion
    }
}
