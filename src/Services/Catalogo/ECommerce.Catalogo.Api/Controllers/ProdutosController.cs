using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Api.Controllers
{
    [Authorize]
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
        [HttpGet("buscar/{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(string nome, int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosFiltradosPaginadosQuery(p => p.Nome.Contains(nome), pagina, linhas));
            
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosPaginadosQuery(pagina, linhas));
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
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
                return BadRequest(resultado);

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
                return BadRequest(resultado);

            return Ok();
        }
    }
}
