using ECommerce.Pedido.Application.Commands;
using ECommerce.Pedido.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Pedido.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(AdicionarPedidoCommand request)
        {
            var validationResult = await _mediator.Send(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("buscar")]
        public async Task<IActionResult> Buscar(BuscarPedidoPorIdQuery request)
        {
            var pedido = await _mediator.Send(request);

            return Ok(pedido);
        }
    }
}
