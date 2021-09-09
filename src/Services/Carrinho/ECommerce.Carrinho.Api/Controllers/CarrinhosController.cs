using ECommerce.Carrinho.Domain.Interfaces.Repositories;
using ECommerce.Carrinho.Domain.Models;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpGet("buscar-carrinho")]
        public async Task<IActionResult> Buscar()
        {
            var carrinho = await BuscarCarrinho();

            return Ok(carrinho);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpPost("adicionar-item")]
        public async Task<IActionResult> Adicionar(ItemCarrinho item)
        {
            var carrinho = await BuscarCarrinho();
            
            if (carrinho == null)
            {
                var novoCarrinho = new Domain.Models.Carrinho(Guid.Parse(UserId()));
                novoCarrinho.AtualizarItem(item);

                var validacao = novoCarrinho.Validar();

                if (!validacao.IsValid)
                    return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoRepository.Adicionar(novoCarrinho);
            }
            else
            {
                carrinho.AtualizarItem(item);

                var validacao = carrinho.Validar();

                if (!validacao.IsValid)
                    return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoRepository.Atualizar(carrinho);
            }

            await _carrinhoRepository.UnitOfWork.Commit();

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        [HttpDelete("remover-item/{produtoId:Guid}")]
        public async Task<IActionResult> Remover(Guid produtoId)
        {
            await _itemRepository.ExcluirPorProdutoId(produtoId);
            await _itemRepository.UnitOfWork.Commit();

            return NoContent();
        }

        #region Métodos auxiliares
        private string UserId() => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        private async Task<Domain.Models.Carrinho> BuscarCarrinho()
        {
            var userId = UserId();

            return await _carrinhoRepository.BuscarPorId(Guid.Parse(userId)) ?? new Domain.Models.Carrinho(Guid.Parse(userId));
        }
        #endregion
    }
}
