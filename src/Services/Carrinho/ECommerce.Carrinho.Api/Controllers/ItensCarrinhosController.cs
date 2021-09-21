using ECommerce.Carrinho.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItensCarrinhosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(AdicionarItemCarrinhoCommand request)
        {
            var validationResult = await _mediator.Send(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir(ExcluirItemCarrinhoCommand request)
        {
            var validationResult = await _mediator.Send(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return NoContent();
        }
    }
}
