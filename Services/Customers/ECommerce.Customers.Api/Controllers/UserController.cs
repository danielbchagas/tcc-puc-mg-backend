using ECommerce.Customer.Api.Constants;
using ECommerce.Customers.Application.Commands.Customer;
using ECommerce.Customers.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Customer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUserQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Item1.IsValid)
                return BadRequest(result.Item1.Errors.Select(e => e.ErrorMessage));

            return Ok(result.Item2);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerCommand request)
        {
            if (id != request.Id)
                return BadRequest(ResponseMessages.InconsistentIdentifiers);

            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("disable/{id:Guid}")]
        public async Task<IActionResult> Disable(Guid id)
        {
            var result = await _mediator.Send(new DisableCustomerCommand(id));

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }
    }
}
