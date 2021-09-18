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

        [HttpPost]
        public IActionResult PagarComCartao(CartaoDto cartao)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PagarComBitcoint(BitcoinDto bitcoin)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PagarComBoleto(BoletoDto boleto)
        {
            return Ok();
        }
    }
}
