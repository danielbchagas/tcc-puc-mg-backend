using ECommerce.Clientes.Domain.Application.Commands.Endereco;
using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IBuscarEnderecoPorIdQuery _query;
        private readonly IBuscarEnderecosFiltradosPaginadosQuery _query2;
        private readonly IBuscarEnderecosPaginadosQuery _query3;
        private readonly IMediator _mediator;

        public EnderecoController(IBuscarEnderecoPorIdQuery query, IBuscarEnderecosFiltradosPaginadosQuery query2, IBuscarEnderecosPaginadosQuery query3, IMediator mediator)
        {
            _query = query;
            _query2 = query2;
            _query3 = query3;
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-todos")]
        public async Task<IActionResult> BuscarTodos(int? pagina, int? linhas)
        {
            var produtos = await _query3.Buscar(pagina, linhas);
            return Ok(produtos);
        }

        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-por-id/{id:length(36)}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var produto = await _query.Buscar(id);
            return Ok(produto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("novo")]
        public async Task<IActionResult> Novo(CadastrarEnderecoCommand request)
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
        public async Task<IActionResult> Atualizar(AtualizarEnderecoCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir(DesativarEnderecoCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }
    }
}
