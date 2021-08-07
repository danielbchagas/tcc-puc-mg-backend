using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Cliente.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<Domain.Models.Cliente>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> BuscarTodos(int pagina, int linhas)
        {
            return Ok(await _mediator.Send(new BuscarClientesPaginadosQuery(pagina, linhas)));
        }

        [ProducesResponseType(typeof(IEnumerable<Domain.Models.Cliente>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("pesquisar/{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Pesquisar(string nome, int pagina, int linhas)
        {
            return Ok(await _mediator.Send(new BuscarClientesFiltradosPaginadosQuery(c => c.Nome.Contains(nome), pagina, linhas)));
        }

        [ProducesResponseType(typeof(Domain.Models.Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-por-id/{id:Guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            return Ok(await _mediator.Send(new BuscarClientePorIdQuery(id)));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("novo")]
        public async Task<IActionResult> Novo(AdicionarClienteCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarClienteCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("desativar")]
        public async Task<IActionResult> Desativar(DesativarClienteCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }
    }
}
