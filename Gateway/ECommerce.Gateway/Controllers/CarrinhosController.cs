using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Carrinho;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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

        public CarrinhosController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var result = await _carrinhoService.BuscarCarrinho(id, accessToken);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return Ok(result.Content);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Create(CarrinhoDto carrinho)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var result = await _carrinhoService.AdicionarCarrinho(carrinho, accessToken);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);
            
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var result = await _carrinhoService.ExcluirCarrinho(id, accessToken);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return NoContent();
        }
    }
}
