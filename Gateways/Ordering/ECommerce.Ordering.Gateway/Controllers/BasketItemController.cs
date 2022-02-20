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
        private readonly IBasketGrpcClient _basketGrpcClient;
        private readonly ICatalogGrpcClient _catalogGrpcClient;

        public BasketItemController(IBasketGrpcClient basketGrpcClient, ICatalogGrpcClient catalogGrpcClient)
        {
            _basketGrpcClient = basketGrpcClient;
            _catalogGrpcClient = catalogGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItemDto item)
        {
            #region Stock update
            var response = await _catalogGrpcClient.GetProduct(new Catalog.Api.Protos.GetProductRequest
            {
                Id = Convert.ToString(item.ProductId)
            });
            
            var basketItem = await _basketGrpcClient.GetBasketItemByProduct(new Basket.Api.Protos.GetBasketItemByProductRequest
            {
                Produtid = Convert.ToString(item.ProductId)
            });

            // TO-DO: Catalog and basket operation

            if (response == null)
                return NotFound();

            if (response.Product.Quantity < item.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");

            var createBasketItemResult = await _catalogGrpcClient.UpdateProduct(new Catalog.Api.Protos.UpdateProductRequest
            {
                Id = response.Product.Id,
                Name = response.Product.Name,
                Description = response.Product.Description,
                Image = response.Product.Image,
                Value = response.Product.Quantity,
                Quantity = response.Product.Quantity,
                Enabled  = response.Product.Enabled
            });

            if (!createBasketItemResult.Isvalid)
                return BadRequest(createBasketItemResult.Message);
            #endregion

            #region Basket update
            var _createBasketItemResult = await _basketGrpcClient.AddBasketItem(new Basket.Api.Protos.AddBasketItemRequest
            {
                Id = Convert.ToString(item.Id),
                Name = response.Product.Name,
                Quantity = item.Quantity,
                Image = response.Product.Image,
                Value = response.Product.Value,
                Productid = response.Product.Id,
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
