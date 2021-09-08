using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Models;
using ECommerce.Carrinho.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhosController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IItemCarrinhoRepository _itemRepository;
        
        public CarrinhosController(IHttpContextAccessor httpContextAccessor, ICarrinhoRepository carrinhoClienteRepository, IItemCarrinhoRepository itemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _carrinhoRepository = carrinhoClienteRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar()
        {
            var carrinho = await BuscarCarrinho();

            return Ok(carrinho);
        }

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> Adicionar(ItemCarrinho item)
        {
            var carrinho = await BuscarCarrinho();
            
            if (carrinho == null)
            {
                var novoCarrinho = new Models.Carrinho(Guid.Parse(UserId()));
                novoCarrinho.AtualizarItem(item);

                if (!novoCarrinho.Validacao.IsValid)
                    return BadRequest(novoCarrinho.Validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoRepository.Adicionar(novoCarrinho);
            }
            else
            {
                carrinho.AtualizarItem(item);

                if (!carrinho.Validacao.IsValid)
                    return BadRequest(carrinho.Validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoRepository.Atualizar(carrinho);
            }

            await _carrinhoRepository.UnitOfWork.Commit();

            return Ok();
        }

        [HttpDelete("remover/{produtoId:Guid}")]
        public async Task<IActionResult> Remover(Guid produtoId)
        {
            await _itemRepository.ExcluirPorProdutoId(produtoId);
            await _itemRepository.UnitOfWork.Commit();

            return NoContent();
        }

        #region Métodos auxiliares
        private string UserId() => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        private async Task<Models.Carrinho> BuscarCarrinho()
        {
            var userId = UserId();

            return await _carrinhoRepository.BuscarPorId(Guid.Parse(userId)) ?? new Models.Carrinho(Guid.Parse(userId));
        }
        #endregion
    }
}
