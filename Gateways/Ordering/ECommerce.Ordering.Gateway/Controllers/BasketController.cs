using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICatalogService _catalogService;

        public BasketController(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _basketService.GetCustomerBasket(customerId, accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return Ok(response.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerBasketDto basket)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            basket.CustomerId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var response = await _basketService.CreateCustomerBasket(basket, accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);
            
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("basket/{customerId:Guid}")]
        public async Task<IActionResult> DeleteBasket(Guid customerId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var response = await _basketService.DeleteCustomerBasket(customerId, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(BasketItemDto item)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
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
        [HttpDelete("item/{id:Guid}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _basketService.DeleteBasketItem(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
