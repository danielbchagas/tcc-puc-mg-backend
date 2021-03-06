using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Basket.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerBasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("customer/{customerId:Guid}")]
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            var result = await _mediator.Send(new GetShoppingBasketQuery(customerId));

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var result = await _mediator.Send(new GetShoppingBasketByCustomerQuery(customerId));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateShoppingBasketCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteShoppingBasketCommand(id));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return NoContent();
        }
    }
}
