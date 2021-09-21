using ECommerce.Compras.Gateway.Dtos.Carrinho;
using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhosController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ICatalogoService _catalogoService;
        private readonly ICarrinhoService _carrinhoService;

        public ItensCarrinhosController(IClienteService clienteService, ICatalogoService catalogoService, ICarrinhoService carrinhoService)
        {
            _clienteService = clienteService;
            _catalogoService = catalogoService;
            _carrinhoService = carrinhoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar(AdicionarItemCarrinhoDto dto)
        {
            var validationResult = new ServiceResponse();

            var produto = await _catalogoService.Buscar(dto.ProdutoId);

            #region Validação de item disponível em estoque
            if (produto.QuantidadeEstoque < dto.Quantidade)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Atualização do estoque
            produto.QuantidadeEstoque -= dto.Quantidade;
            validationResult = await _catalogoService.Atualizar(produto);

            if (validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            #endregion

            #region Atualização de carrinho
            dto.Nome = produto.Nome;
            dto.Imagem = produto.Imagem;
            dto.Valor = produto.Valor;

            validationResult = await _carrinhoService.Adicionar(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            #endregion

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir(ExcluirItemCarrinhoDto dto)
        {
            var validationResult = await _carrinhoService.Excluir(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            return NoContent();
        }
    }
}
