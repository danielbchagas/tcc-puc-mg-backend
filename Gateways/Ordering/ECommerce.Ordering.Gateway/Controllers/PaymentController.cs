using System;
using ECommerce.Ordering.Gateway.Constants;
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
                return Ok(new { valor = value, mensagem = ResponseMessages.PaymentRefused });

            return Ok(new { valor = value, mensagem = ResponseMessages.PaymentAccepted });
        }

        [HttpPost("bitcoin")]
        public IActionResult Bitcoin(decimal value)
        {
            if (Approved())
                return Ok(new { valor = value, mensagem = ResponseMessages.PaymentRefused });

            return Ok(new { valor = value, mensagem = ResponseMessages.PaymentAccepted });
        }

        [HttpPost("bank-bill")]
        public IActionResult BankBill(decimal value)
        {
            return Ok(new { valor = value, mensagem = ResponseMessages.PaymentIsProcessing });
        }

        private bool Approved()
        {
            return new Random().Next(0, 1) != 0;
        }
    }
}
