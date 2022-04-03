using ECommerce.Ordering.Gateway.Constants;
using ECommerce.Ordering.Gateway.DTOs.Request;
using ECommerce.Ordering.Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketGrpcClient _basketGrpcClient;
        private readonly ICatalogGrpcClient _catalogGrpcClient;

        public BasketController(IBasketGrpcClient basketGrpcClient, ICatalogGrpcClient catalogGrpcClient)
        {
            _basketGrpcClient = basketGrpcClient;
            _catalogGrpcClient = catalogGrpcClient;
        }

        #region Shopping Basket
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("customer/{customerId:Guid}")]
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            var response = await _basketGrpcClient.GetAllShoppingBasket(new Basket.Api.Protos.GetAllBasketRequest
            {
                Customerid = Convert.ToString(customerId)
            });

            return Ok(response.Baskets);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _basketGrpcClient.GetShoppingBasketById(new Basket.Api.Protos.GetBasketByIdRequest
            {
                Id = Convert.ToString(id)
            });
            
            return Ok(response.Basket);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBasketRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var newBasket = new ECommerce.Basket.Api.Protos.CreateBasketRequest
            {
                Id = Convert.ToString(request.Id == Guid.Empty ? Guid.NewGuid() : request.Id),
                Customerid = Convert.ToString(request.CustomerId)
            };

            var response = await _basketGrpcClient.CreateShoppingBasket(newBasket);

            if (!response.Isvalid)
                return BadRequest(response.Message);

            return Ok(newBasket);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateBasketRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseMessages.InconsistentIdentifiers);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            var newBasket = new ECommerce.Basket.Api.Protos.UpdateBasketRequest
            {
                Id = Convert.ToString(request.Id == Guid.Empty ? Guid.NewGuid() : request.Id),
                Isended = request.IsEnded,
                Customerid = Convert.ToString(request.CustomerId)
            };

            var response = await _basketGrpcClient.UpdateShoppingBasket(newBasket);

            if (!response.Isvalid)
                return BadRequest(response.Message);

            return Ok(newBasket);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _basketGrpcClient.DeleteShoppingBasket(new Basket.Api.Protos.DeleteBasketRequest
            {
                Id = Convert.ToString(id)
            });

            if (!response.Isvalid)
                return BadRequest(response.Message);

            return NoContent();
        }
        #endregion

        #region Basket Item
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}/item")]
        public async Task<IActionResult> UpdateItem(Guid id, CreateBasketItemRequest request)
        {
            if (id != request.BasketId)
                return BadRequest(ResponseMessages.InconsistentIdentifiers);

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)));

            #region Get values
            var product = (await _catalogGrpcClient.GetProduct(new Catalog.Api.Protos.GetProductRequest
            {
                Id = Convert.ToString(request.ProductId)
            })).Product;

            if (product == null)
                return NotFound();

            var currentItem = (await _basketGrpcClient.GetBasketItemByProduct(new Basket.Api.Protos.GetBasketItemByProductRequest
            {
                Produtid = Convert.ToString(request.ProductId)
            })).Item;
            #endregion

            #region Stock update
            product.Quantity = UpdateProductQuantity(
                stockQuantity: product.Quantity,
                newQuantity: request.Quantity,
                currentQuantity: currentItem != null ? currentItem.Quantity : 0
            );

            if (product.Quantity < request.Quantity)
                return BadRequest(ResponseMessages.OutOfStock);

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

            #region Basket update
            var _createBasketItemResult = await _basketGrpcClient.AddBasketItem(new Basket.Api.Protos.AddBasketItemRequest
            {
                Id = Convert.ToString(request.Id == Guid.Empty ? Guid.NewGuid() : request.Id),
                Name = product.Name,
                Quantity = request.Quantity,
                Image = product.Image,
                Value = product.Value,
                Productid = product.Id,
                Shoppingbasketid = Convert.ToString(request.BasketId),
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
        [HttpDelete("{id:Guid}/item/{basketItemId:Guid}")]
        public async Task<IActionResult> DeleteItem(Guid id, Guid basketItemId)
        {
            #region Get Values
            var currentItem = (await _basketGrpcClient.GetBasketItem(new Basket.Api.Protos.GetBasketItemRequest
            {
                Id = Convert.ToString(basketItemId)
            })).Item;

            if (currentItem == null)
                return BadRequest(ResponseMessages.InvalidOperation);

            var product = (await _catalogGrpcClient.GetProduct(new Catalog.Api.Protos.GetProductRequest
            {
                Id = Convert.ToString(currentItem.Productid)
            })).Product;

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
                Id = Convert.ToString(basketItemId),
                Basketid = Convert.ToString(id)
            });

            if (!removeBasketItemResult.Isvalid)
                return BadRequest(removeBasketItemResult.Message);

            return NoContent();
            #endregion
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
