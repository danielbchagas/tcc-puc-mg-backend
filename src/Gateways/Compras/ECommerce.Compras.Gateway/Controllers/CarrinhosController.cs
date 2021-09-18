using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Carrinho;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhosController : ControllerBase
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IClienteService _clienteService;
        private readonly ICatalogoService _catalogoService;
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhosController(IAspNetUser aspNetUser, IClienteService clienteService, ICatalogoService catalogoService, ICarrinhoService carrinhoService)
        {
            _aspNetUser = aspNetUser;
            _clienteService = clienteService;
            _catalogoService = catalogoService;
            _carrinhoService = carrinhoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-carrinho")]
        public async Task<IActionResult> Buscar()
        {
            var carrinho = await _carrinhoService.Buscar();

            return Ok(carrinho);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar-item")]
        public async Task<IActionResult> Adicionar(ItemCarrinho item)
        {
            var produto = await _catalogoService.Buscar(item.ProdutoId);

            #region Validação de item disponível em estoque
            if (produto.QuantidadeEstoque < item.Quantidade)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Atualização do estoque
            produto.QuantidadeEstoque -= item.Quantidade;
            var catalogoServiceResult = await _catalogoService.Atualizar(produto);

            if (catalogoServiceResult.IsValid)
                return BadRequest("Houve uma falha com a operação. Por favor, entre em contato com o suporte.");
            #endregion

            #region Atualização de carrinho
            item.Nome = produto.Nome;
            item.Imagem = produto.Imagem;
            item.Valor = produto.Valor;

            var carrinhoResult = await _carrinhoService.Adicionar(item);

            if (!carrinhoResult.IsValid)
                return BadRequest(carrinhoResult.Errors.Select(e => e.ErrorMessage));
            #endregion

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("excluir-item/{produtoId:Guid}")]
        public async Task<IActionResult> Excluir(Guid produtoId)
        {
            await _carrinhoService.Excluir(produtoId);

            return NoContent();
        }
    }
}
