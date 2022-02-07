﻿using ECommerce.Core.Models.Basket;
using ECommerce.Ordering.Gateway.Interfaces;
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

        public BasketController(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> GetBasket(Guid customerId)
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
        public async Task<IActionResult> CreateBasket(CustomerBasket basket)
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
        [HttpDelete("{customerId:Guid}")]
        public async Task<IActionResult> DeleteBasket(Guid customerId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var response = await _basketService.DeleteCustomerBasket(customerId, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            
            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
