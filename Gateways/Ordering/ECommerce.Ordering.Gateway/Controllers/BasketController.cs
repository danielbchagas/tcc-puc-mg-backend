using ECommerce.Basket.Domain.Models;
using ECommerce.Ordering.Gateway.Interfaces;
using ECommerce.Ordering.Gateway.Models;
using ECommerce.Ordering.Gateway.Services.gRPC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketGrpcClient _basketGrpcClient;

        public BasketController(IBasketService basketService, IHttpContextAccessor httpContextAccessor, IBasketGrpcClient basketGrpcClient)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
            _basketGrpcClient = basketGrpcClient;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var accessToken = await GetToken();

            //var response = await _basketService.GetCustomerBasket(customerId, accessToken);

            //if (!response.IsSuccessStatusCode)
            //    return BadRequest(response.Error);

            //return Ok(response.Content);

            var response = await _basketGrpcClient.GetCustomerBasket(customerId);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerBasketDTO basket)
        {
            var accessToken = await GetToken();

            var response = await _basketService.CreateCustomerBasket(new CustomerBasket(basket.CustomerId), accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);
            
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("basket/{customerId:Guid}")]
        public async Task<IActionResult> DeleteBasket(Guid customerId)
        {
            var accessToken = await GetToken();

            var response = await _basketService.DeleteCustomerBasket(customerId, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }

        #region Helpers
        protected async Task<string> GetToken()
            => await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        #endregion
    }
}
