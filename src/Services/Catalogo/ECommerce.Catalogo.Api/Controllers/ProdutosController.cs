using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-todos/{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> BuscarTodos(string nome, int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosFiltradosPaginadosQuery(p => p.Nome.Contains(nome), pagina, linhas));
            
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-todos")]
        public async Task<IActionResult> BuscarTodos()
        {
            var produtos = await _mediator.Send(new BuscarProdutosQuery());

            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-todos/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> BuscarTodos(int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosPaginadosQuery(pagina, linhas));
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-por-id/{id:Guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var produto = await _mediator.Send(new BuscarProdutoPorIdQuery(id));
            return Ok(produto);
        }
        
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(AdicionarProdutoCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarProdutoCommand request)
        {
            var resultado = await _mediator.Send(request);

            if (!resultado.IsValid)
                return BadRequest(resultado.Errors.Select(e => e.ErrorMessage));

            return Ok();
        }
    }
}
