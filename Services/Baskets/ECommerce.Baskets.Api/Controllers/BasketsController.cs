using ECommerce.Baskets.Application.Commands.Basket;
using ECommerce.Baskets.Application.Commands.Item;
using ECommerce.Baskets.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetBasketByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBasketCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Item1.IsValid)
                return BadRequest(result.Item1.Errors.Select(e => e.ErrorMessage));

            return Ok(result.Item2);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}/items")]
        public async Task<IActionResult> Put(Guid id, IncludeItemCommand request)
        {
            if (id != request.BasketId)
                return BadRequest();

            var result = await _mediator.Send(request);

            if (!result.Item1.IsValid)
                return BadRequest(result.Item1.Errors.Select(e => e.ErrorMessage));

            return Ok(result.Item2);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DisableBasketCommand(id));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}/items")]
        public async Task<IActionResult> DeleteItem(Guid id, RemoveItemCommand request)
        {
            if (id != request.BasketId)
                return BadRequest();

            var result = await _mediator.Send(new RemoveItemCommand(id));

            if (!result.Item1.IsValid)
                return BadRequest(result.Item1.Errors.Select(e => e.ErrorMessage));

            return NoContent();
        }
    }
}
