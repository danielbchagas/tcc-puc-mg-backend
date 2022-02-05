using System;
using System.Net;
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
    public class OrderingController : ControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderingController(IOrderingService orderingService, IHttpContextAccessor httpContextAccessor)
        {
            _orderingService = orderingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _orderingService.GetOrder(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return Ok(response.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto order)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _orderingService.Create(order, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error);

            return Ok();
        }
    }
}
