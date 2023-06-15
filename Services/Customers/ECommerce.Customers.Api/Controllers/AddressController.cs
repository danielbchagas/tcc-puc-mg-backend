using ECommerce.Customer.Api.Constants;
using ECommerce.Customers.Application.Commands.Address;
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
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.Item1.IsValid)
                return BadRequest(result.Item1.Errors.Select(e => e.ErrorMessage));

            return Ok(result.Item2);
        }
    }
}
