using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TelefonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var documento = await _mediator.Send(new BuscarTelefonePorIdQuery(id));

            return Ok(documento);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost]
        public async Task<IActionResult> Adicionar(AdicionarTelefoneCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarTelefoneCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }
    }
}
