using ECommerce.Compras.Gateway.Dtos.Carrinho;
using ECommerce.Compras.Gateway.Models.Pagamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        public PagamentosController()
        {

        }

        [HttpPost("pagamento-cartao")]
        public IActionResult PagarComCartao(CartaoDto cartao)
        {
            return Ok();
        }

        [HttpPost("pagamento-bitcoin")]
        public IActionResult PagarComBitcoin(BitcoinDto bitcoin)
        {
            return Ok();
        }

        [HttpPost("pagamento-boleto")]
        public IActionResult PagarComBoleto(BoletoDto boleto)
        {
            return Ok();
        }

        [HttpPost("finalizar-compra")]
        public IActionResult Finalizar(ExcluirCarrinhoDto dto)
        {
            return RedirectToAction("excluir", "carrinhos", dto);
        }
    }
}
