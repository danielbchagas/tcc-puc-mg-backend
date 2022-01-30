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
            _aprovado = new Random().Next(0, 1) != 0;
        }

        [HttpPost("credit-card")]
        public IActionResult CreditCard(decimal value)
        {
            if (!_aprovado)
                return Ok(new { valor = value, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = value, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("bitcoin")]
        public IActionResult Bitcoin(decimal value)
        {
            if (!_aprovado)
                return Ok(new { valor = value, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = value, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("bank-bill")]
        public IActionResult BankBill(decimal value)
        {
            return Ok(new { valor = value, mensagem = "Pagamento em processamento..." });
        }
    }
}
