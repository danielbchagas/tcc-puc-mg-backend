using ECommerce.Carrinho.Api.Interfaces;
using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhosController : ControllerBase
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IItemCarrinhoRepository _itemRepository;
        
        public CarrinhosController(IAspNetUser aspNetUser, ICarrinhoRepository carrinhoClienteRepository, IItemCarrinhoRepository itemRepository)
        {
            _aspNetUser = aspNetUser;
            _carrinhoRepository = carrinhoClienteRepository;
            _itemRepository = itemRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-carrinho")]
        public async Task<IActionResult> Buscar()
        {
            var carrinho = await _carrinhoRepository.BuscarPorClienteId(_aspNetUser.ObterUserId());

            return Ok(carrinho);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar-item")]
        public async Task<IActionResult> Adicionar(ItemCarrinho item)
        {
            var userId = _aspNetUser.ObterUserId();

            var carrinho = await _carrinhoRepository.BuscarPorClienteId(userId) ?? new CarrinhoCliente(userId);
            carrinho.AtualizarItensCarrinho(item);
            
            var validationResult = carrinho.Validar();

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            // Se o carrinho existe, adiciona
            if (await _carrinhoRepository.BuscarPorClienteId(userId) == null)
                await _carrinhoRepository.Adicionar(carrinho);
            else // Se não, atualiza
                await _carrinhoRepository.Atualizar(carrinho);

            await _carrinhoRepository.UnitOfWork.Commit();

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("excluir-item/{produtoId:Guid}")]
        public async Task<IActionResult> Excluir(Guid produtoId)
        {
            await _itemRepository.ExcluirPorProdutoId(produtoId);
            await _itemRepository.UnitOfWork.Commit();

            return NoContent();
        }
    }
}
