using ECommerce.Produtos.Domain.Application.Commands;
using ECommerce.Produtos.Domain.Interfaces.Queries;
using ECommerce.Produtos.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoQuery _query;
        private readonly IMediator _mediator;

        public ProdutosController(IProdutoQuery query, IMediator mediator)
        {
            _query = query;
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-todos")]
        public async Task<IActionResult> BuscarTodos(int? pagina, int? linhas)
        {
            var produtos = await _query.Buscar(pagina, linhas);
            return Ok(produtos);
        }

        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
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
        public async Task<IActionResult> Novo(RegistrarProdutoCommand request)
        {
            request.OrigemRequisicao = HttpContext.Connection.RemoteIpAddress.ToString();
            request.Uri = HttpContext.Request.Path;
            
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarProdutoCommand request)
        {
            request.OrigemRequisicao = HttpContext.Connection.RemoteIpAddress.ToString();
            request.Uri = HttpContext.Request.Path;

            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir(ExcluirProdutoCommand request)
        {
            request.OrigemRequisicao = HttpContext.Connection.RemoteIpAddress.ToString();
            request.Uri = HttpContext.Request.Path;

            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return NoContent();
        }
    }
}
