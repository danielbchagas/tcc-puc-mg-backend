using System;
using ECommerce.Ordering.Gateway.Constants;
using ECommerce.Ordering.Gateway.DTOs.Response;
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
            if (!Approved())
                return Ok(new PaymentResponse { Approved = false, Value = value, Message = ResponseMessages.PaymentRefused });

            return Ok(new PaymentResponse { Approved = true, Value = value, Message = ResponseMessages.PaymentAccepted });
        }

        [HttpPost("bitcoin")]
        public IActionResult Bitcoin(decimal value)
        {
            if (!Approved())
                return Ok(new PaymentResponse { Approved = false, Value = value, Message = ResponseMessages.PaymentRefused });

            return Ok(new PaymentResponse { Approved = true, Value = value, Message = ResponseMessages.PaymentAccepted });
        }

        [HttpPost("bank-bill")]
        public IActionResult BankBill(decimal value)
        {
            return Ok(new PaymentResponse { Approved = false, Value = value, Message = ResponseMessages.PaymentIsProcessing });
        }

        private bool Approved()
        {
            return new Random().Next(0, 1) != 0;
        }
    }
}
