using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Queries;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProdutosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-todos/{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> BuscarTodos(string nome, int? pagina, int? linhas)
        {
            Expression<Func<Produto, bool>> filtro = null;

            if (!string.IsNullOrEmpty(nome))
            {
                filtro = p => p.Nome.Contains(nome);
            }
            
            var produtos = await _mediator.Send(new BuscarProdutosFiltradosPaginadosQuery(filtro, pagina, linhas));
            return Ok(produtos);
        }

        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("buscar-por-id/{id:Guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var produto = await _mediator.Send(new BuscarProdutoPorIdQuery(id));
            return Ok(produto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("novo")]
        public async Task<IActionResult> Novo(AdicionarProdutoCommand request)
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
        public async Task<IActionResult> Atualizar(AtualizarProdutoCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(_ => _.ErrorMessage));

            return Ok();
        }
    }
}
