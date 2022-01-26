using System;
using System.Threading.Tasks;
using ECommerce.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{product:alpha}/{page:int}/{rows:int}")]
        public async Task<IActionResult> Get(string product, int page, int rows)
        {
            var result = await _mediator.Send(new FilterProductsQuery(p => p.Name.Contains(product), page, rows));
            
            return Ok(result);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{page:int}/{rows:int}")]
        public async Task<IActionResult> Get(int page, int rows)
        {
            var result = await _mediator.Send(new GetProductsQuery(page, rows));

            return Ok(result);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
