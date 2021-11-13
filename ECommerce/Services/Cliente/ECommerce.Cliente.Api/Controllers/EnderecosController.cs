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
    public class EnderecosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnderecosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var endereco = await _mediator.Send(new BuscarEnderecoPorIdQuery(id));

            if (endereco is null)
                return NotFound();

            return Ok(endereco);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Adicionar(AdicionarEnderecoCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarEnderecoCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }
    }
}
