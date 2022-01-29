using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Customers.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            var result = await _mediator.Send(new DisableCustomerCommand(id));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [Authorize("Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }
    }
}
