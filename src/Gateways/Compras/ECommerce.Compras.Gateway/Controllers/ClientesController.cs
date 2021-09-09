using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> Atualizar(AtualizarClienteDto cliente)
        {
            var result = await _clienteService.Atualizar(cliente);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar/{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var response = await _clienteService.Buscar(id);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("desativar/{id:Guid}")]
        public async Task<IActionResult> Desativar(Guid id)
        {
            var result = await _clienteService.Desativar(id);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok();
        }
    }
}
