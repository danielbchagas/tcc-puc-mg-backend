using ECommerce.Compras.Gateway.Models.Pagamento.Request;
using ECommerce.Compras.Gateway.Models.Pagamento.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private bool _aprovado;

        public PagamentosController()
        {
            var rand = new Random();
            _aprovado = rand.Next(0, 1) == 0 ? false : true;
        }

        [HttpPost("pagamento-cartao")]
        public IActionResult PagarComCartao(CartaoDto cartao)
        {
            if (!_aprovado)
                return Ok(new CartaoResponseDto(false, "Limite insuficiente."));

            return Ok(new CartaoResponseDto(true, "Pagamento aprovado."));
        }

        [HttpPost("pagamento-bitcoin")]
        public IActionResult PagarComBitcoin(BitcoinDto bitcoin)
        {
            if (!_aprovado)
                return Ok(new CartaoResponseDto(false, "Saldo insuficiente."));

            return Ok(new CartaoResponseDto(true, "Pagamento aprovado."));
        }

        [HttpPost("pagamento-boleto")]
        public IActionResult PagarComBoleto(BoletoDto boleto)
        {
            return Ok(new CartaoResponseDto(false, "Pagamento em processamento."));
        }
    }
}
