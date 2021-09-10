using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Catalogo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var response = await _catalogoService.Buscar(id);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(int pagina, int linhas)
        {
            var response = await _catalogoService.Buscar(pagina, linhas);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Buscar(string nome, int pagina, int linhas)
        {
            var response = await _catalogoService.Buscar(nome, pagina, linhas);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarProdutoDto produto)
        {
            var result = await _catalogoService.Cadastrar(produto);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarProdutoDto produto)
        {
            var result = await _catalogoService.Atualizar(produto);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }
    }
}
