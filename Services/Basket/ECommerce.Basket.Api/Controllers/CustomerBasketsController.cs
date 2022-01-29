using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Basket.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerBasketsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var result = await _mediator.Send(new GetCustomerBasketByCustomerQuery(customerId));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerBasketCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{customerId:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new DeleteCustomerBasketCommand(id, new Guid(userId)));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return NoContent();
        }
    }
}
