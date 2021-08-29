using ECommerce.Carrinho.Api.Interfaces.Repositories;
using ECommerce.Carrinho.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhosController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICarrinhoClienteRepository _carrinhoClienteRepository;
        private readonly CarrinhoClienteValidator _carrinhoClienteValidator;

        public CarrinhosController(IHttpContextAccessor httpContextAccessor, ICarrinhoClienteRepository carrinhoClienteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _carrinhoClienteRepository = carrinhoClienteRepository;
            _carrinhoClienteValidator = new CarrinhoClienteValidator();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar()
        {
            var carrinho = await BuscarCarrinho() ?? new CarrinhoCliente();

            return Ok(carrinho);
        }

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> Adicionar(ItemCarrinho item)
        {
            var carrinho = await BuscarCarrinho();

            if(carrinho == null)
            {
                var novoCarrinho = new CarrinhoCliente(Guid.Parse(UserId()));
                novoCarrinho.AdicionarItem(item);

                var validacao = _carrinhoClienteValidator.Validate(novoCarrinho);

                if (!validacao.IsValid)
                    return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoClienteRepository.Adicionar(novoCarrinho);
            }
            else
            {
                var validacao = _carrinhoClienteValidator.Validate(carrinho);

                if (!validacao.IsValid)
                    return BadRequest(validacao.Errors.Select(e => e.ErrorMessage));

                await _carrinhoClienteRepository.Atualizar(carrinho);
            }

            await _carrinhoClienteRepository.UnitOfWork.Commit();

            return Ok();
        }

        [HttpPut("atualizar/{produtoId:Guid}")]
        public async Task<IActionResult> Atualizar(Guid produtoId, ItemCarrinho item)
        {
            return Ok();
        }

        [HttpDelete("remover/{produtoId:Guid}")]
        public async Task<IActionResult> Remover(Guid produtoId)
        {
            return NoContent();
        }

        private string UserId() => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        private async Task<CarrinhoCliente> BuscarCarrinho()
        {
            var userId = UserId();

            return await _carrinhoClienteRepository.BuscarPorId(Guid.Parse(userId));
        }
    }
}
