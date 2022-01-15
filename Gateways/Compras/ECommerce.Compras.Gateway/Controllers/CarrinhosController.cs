using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Carrinho;
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
    public class CarrinhosController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly IAspNetUser _aspNetUser;

        public CarrinhosController(ICarrinhoService carrinhoService, IAspNetUser aspNetUser)
        {
            _carrinhoService = carrinhoService;
            _aspNetUser = aspNetUser;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Buscar(Guid id)
        {
            var token = _aspNetUser.ObterUserToken();
            var result = await _carrinhoService.BuscarCarrinho(id, token);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return Ok(result.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Adicionar(CarrinhoDto carrinho)
        {
            var token = _aspNetUser.ObterUserToken();
            var result = await _carrinhoService.AdicionarCarrinho(carrinho, token);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);
            
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var token = _aspNetUser.ObterUserToken();
            var result = await _carrinhoService.ExcluirCarrinho(id, token);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return NoContent();
        }
    }
}
