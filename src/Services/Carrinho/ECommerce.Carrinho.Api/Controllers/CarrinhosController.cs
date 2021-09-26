using ECommerce.Carrinho.Application.Commands;
using ECommerce.Carrinho.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarrinhosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var carrinho = await _mediator.Send(new BuscarCarrinhoPorClienteQuery(id));

            return Ok(carrinho);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(AdicionarCarrinhoCommand request)
        {
            var validationResult = await _mediator.Send(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return Ok();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var validationResult = await _mediator.Send(new ExcluirCarrinhoCommand(id));

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return NoContent();
        }

        
    }
}
