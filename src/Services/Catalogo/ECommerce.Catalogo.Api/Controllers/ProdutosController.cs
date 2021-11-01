using ECommerce.Catalogo.Application.Queries;
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
        [HttpGet("{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(string nome, int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosFiltradosPaginadosQuery(p => p.Nome.Contains(nome), pagina, linhas));
            
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(int pagina, int linhas)
        {
            var produtos = await _mediator.Send(new BuscarProdutosPaginadosQuery(pagina, linhas));
            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var produto = await _mediator.Send(new BuscarProdutoPorIdQuery(id));

            if (produto is null)
                return NotFound();

            return Ok(produto);
        }
    }
}
