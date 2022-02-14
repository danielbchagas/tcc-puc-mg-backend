using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Ordering.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("credit-card")]
        public IActionResult CreditCard(decimal value)
        {
            if (Approved())
                return Ok(new { valor = value, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = value, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("bitcoin")]
        public IActionResult Bitcoin(decimal value)
        {
            if (Approved())
                return Ok(new { valor = value, mensagem = "Saldo insuficiente." });

            return Ok(new { valor = value, mensagem = "Pagamento aprovado." });
        }

        [HttpPost("bank-bill")]
        public IActionResult BankBill(decimal value)
        {
            return Ok(new { valor = value, mensagem = "Pagamento em processamento..." });
        }

        private bool Approved()
        {
            return new Random().Next(0, 1) != 0;
        }
    }
}
