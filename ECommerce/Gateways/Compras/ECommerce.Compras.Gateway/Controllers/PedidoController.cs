using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Pedido;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IAspNetUser _aspNetUser;

        public PedidoController(IPedidoService pedidoService, IAspNetUser aspNetUser)
        {
            _pedidoService = pedidoService;
            _aspNetUser = aspNetUser;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var token = _aspNetUser.ObterUserToken();
            var result = await _pedidoService.Buscar(id, token);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Adicionar(PedidoDto pedido)
        {
            var token = _aspNetUser.ObterUserToken();
            var result = await _pedidoService.Adicionar(pedido, token);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return Ok();
        }
    }
}
