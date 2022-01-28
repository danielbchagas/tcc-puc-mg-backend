using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerce.Gateway.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private bool _aprovado;

        public PaymentController()
        {
            _aprovado = new Random().Next(0, 1) == 0 ? false : true;
        }

        [HttpPost("pagamento-cartao")]
        public IActionResult PagarComCartao(decimal valor)
        {
            if (!_aprovado)
                return Ok(new { valor = valor, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = valor, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("pagamento-bitcoin")]
        public IActionResult PagarComBitcoin(decimal valor)
        {
            if (!_aprovado)
                return Ok(new { valor = valor, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = valor, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("pagamento-boleto")]
        public IActionResult PagarComBoleto(decimal valor)
        {
            return Ok(new { valor = valor, mensagem = "Pagamento em processamento..." });
        }
    }
}
