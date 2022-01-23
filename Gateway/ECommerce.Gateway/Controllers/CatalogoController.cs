using ECommerce.Compras.Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{nome:alpha}/{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Get(string nome, int pagina, int linhas)
        {
            var produtos = await _catalogoService.Get(nome, pagina, linhas);

            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{pagina:int}/{linhas:int}")]
        public async Task<IActionResult> Get(int pagina, int linhas)
        {
            var produtos = await _catalogoService.Get(pagina, linhas);

            return Ok(produtos);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _catalogoService.Get(id);

            if (produto is null)
                return NotFound();

            return Ok(produto);
        }
    }
}
