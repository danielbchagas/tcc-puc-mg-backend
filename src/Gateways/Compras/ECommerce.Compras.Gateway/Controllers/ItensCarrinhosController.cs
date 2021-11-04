﻿using ECommerce.Compras.Gateway.Interfaces;
using ECommerce.Compras.Gateway.Models.Carrinho;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCarrinhosController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICarrinhoService _carrinhoService;

        public ItensCarrinhosController(ICatalogoService catalogoService, ICarrinhoService carrinhoService)
        {
            _catalogoService = catalogoService;
            _carrinhoService = carrinhoService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Adicionar(ItemCarrinhoDto item)
        {
            var produto = (await _catalogoService.Buscar(item.ProdutoId)).Content;

            #region Validação de item disponível em estoque
            if (produto.QuantidadeEstoque < item.Quantidade)
                return BadRequest("Quantidade indisponível em estoque!");
            #endregion

            #region Atualização do estoque
            produto.QuantidadeEstoque -= item.Quantidade;
            var result = await _catalogoService.Atualizar(produto);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            #region Atualização de carrinho
            item.Nome = produto.Nome;
            item.Imagem = produto.Imagem;
            item.Valor = produto.Valor;

            result = await _carrinhoService.AdicionarItemCarrinho(item);

            if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error);
            #endregion

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var result = await _carrinhoService.ExcluirItemCarrinho(id);

            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            else if (!result.IsSuccessStatusCode)
                return BadRequest(result.Error.Content);

            return NoContent();
        }
    }
}
