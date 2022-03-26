using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Customer.Api.Constants;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Customer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetEmailQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmailCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateEmailCommand request)
        {
            if (id != request.Id)
                return BadRequest(ResponseMessages.InconsistentIdentifiers);

            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }
    }
}
