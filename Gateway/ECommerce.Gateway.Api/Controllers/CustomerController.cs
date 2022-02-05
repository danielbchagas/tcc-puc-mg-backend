using ECommerce.Gateway.Api.Interfaces;
using ECommerce.Gateway.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _customerService.GetCustomer(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error.Content);

            return Ok(response.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Update(UserDto user)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _customerService.UpdateCustomer(user, accessToken);

            if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error.Content);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var response = await _customerService.DeleteCustomer(id, accessToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!response.IsSuccessStatusCode)
                return BadRequest(response.Error.Content);

            return NoContent();
        }
    }
}
