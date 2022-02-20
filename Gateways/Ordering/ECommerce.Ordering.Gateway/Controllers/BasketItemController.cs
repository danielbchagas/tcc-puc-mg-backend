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
        public async Task<IActionResult> Create(BasketItemDto newItem)
        {
            #region Get values
            var product = await GetProduct(newItem.ProductId);

            if (product == null)
                return NotFound();

            var currentItem = await GetBasketItemByProduct(newItem.ProductId);

            if (currentItem != null && (currentItem.Quantity == newItem.Quantity))
                return BadRequest("Operação inválida!");
            #endregion

            #region Stock update
            product.Quantity = UpdateProductQuantity(
                stockQuantity: product.Quantity, 
                newQuantity: newItem.Quantity,
                currentQuantity: currentItem != null ? currentItem.Quantity : 0
            );

            if (product.Quantity < newItem.Quantity)
                return BadRequest("Quantidade indisponível em estoque!");

            var createBasketItemResult = await _catalogGrpcClient.UpdateProduct(new Catalog.Api.Protos.UpdateProductRequest
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Value = product.Value,
                Quantity = product.Quantity,
                Enabled  = product.Enabled
            });

            if (!createBasketItemResult.Isvalid)
                return BadRequest(createBasketItemResult.Message);
            #endregion

            #region Basket update
            var _createBasketItemResult = await _basketGrpcClient.AddBasketItem(new Basket.Api.Protos.AddBasketItemRequest
            {
                Id = Convert.ToString(newItem.Id),
                Name = product.Name,
                Quantity = newItem.Quantity,
                Image = product.Image,
                Value = product.Value,
                Productid = product.Id,
                Shoppingbasketid = Convert.ToString(newItem.BasketId),
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
            #region Get Values
            var currentItem = await GetBasketItem(id);

            if (currentItem == null)
                return BadRequest("Operação inválida!");

            var product = await GetProduct(Guid.Parse(currentItem.Productid));

            if (product == null)
                return NotFound();
            #endregion

            #region Catalog update
            product.Quantity += currentItem.Quantity;

            var createBasketItemResult = await _catalogGrpcClient.UpdateProduct(new Catalog.Api.Protos.UpdateProductRequest
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Value = product.Value,
                Quantity = product.Quantity,
                Enabled = product.Enabled
            });

            if (!createBasketItemResult.Isvalid)
                return BadRequest(createBasketItemResult.Message);
            #endregion

            #region Delete Basket Item
            var removeBasketItemResult = await _basketGrpcClient.RemoveBasketItem(new Basket.Api.Protos.RemoveBasketItemRequest
            {
                Id = Convert.ToString(id)
            });

            if (!removeBasketItemResult.Isvalid)
                return BadRequest(removeBasketItemResult.Message);

            return NoContent();
            #endregion
        }

        #region Helpers
        private async Task<Catalog.Api.Protos.Product> GetProduct(Guid productId)
        {
            var catalogResponse = await _catalogGrpcClient.GetProduct(new Catalog.Api.Protos.GetProductRequest
            {
                Id = Convert.ToString(productId)
            });

            return catalogResponse.Product;
        }

        private async Task<Basket.Api.Protos.BasketItem> GetBasketItemByProduct(Guid productId)
        {
            var basketResponse = await _basketGrpcClient.GetBasketItemByProduct(new Basket.Api.Protos.GetBasketItemByProductRequest
            {
                Produtid = Convert.ToString(productId)
            });

            return basketResponse.Item;
        }

        private async Task<Basket.Api.Protos.BasketItem> GetBasketItem(Guid id)
        {
            var basketResponse = await _basketGrpcClient.GetBasketItem(new Basket.Api.Protos.GetBasketItemRequest
            {
                Id = Convert.ToString(id)
            });

            return basketResponse.Item;
        }

        private int UpdateProductQuantity(int stockQuantity, int newQuantity, int currentQuantity)
        {
            if (currentQuantity > newQuantity)
                stockQuantity += (currentQuantity - newQuantity);
            else
                stockQuantity -= Math.Abs(currentQuantity - newQuantity);

            return stockQuantity;
        }
        #endregion
    }
}
