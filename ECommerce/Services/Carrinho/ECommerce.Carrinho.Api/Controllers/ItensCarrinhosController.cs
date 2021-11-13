using ECommerce.Carrinho.Api.Interfaces;
using ECommerce.Carrinho.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAspNetUser _aspNetUser;

        public ItensCarrinhosController(IMediator mediator, IAspNetUser aspNetUser)
        {
            _mediator = mediator;
            _aspNetUser = aspNetUser;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Adicionar(AdicionarItemCarrinhoCommand request)
        {
            var validationResult = await _mediator.Send(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var validationResult = await _mediator.Send(new ExcluirItemCarrinhoCommand(id, _aspNetUser.ObterUserId()));

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            return NoContent();
        }
    }
}
