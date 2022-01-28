using ECommerce.Gateway.Api.Interfaces;
using ECommerce.Gateway.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketsController(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _basketService.GetCarrinho(customerId, accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return Ok(response.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerBasket basket)
        {
            basket.CustomerId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _basketService.Create(basket, accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);
            
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{customerId:Guid}")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _basketService.DeleteCarrinho(customerId, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return NoContent();
        }
    }
}
