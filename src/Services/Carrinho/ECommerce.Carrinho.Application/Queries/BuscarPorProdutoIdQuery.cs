using ECommerce.Carrinho.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Queries
{
    public class BuscarPorProdutoIdQuery : IRequest<ItemCarrinho>
    {
        public BuscarPorProdutoIdQuery(Guid produtoId)
        {
            ProdutoId = produtoId;
        }

        public Guid ProdutoId { get; set; }
    }
}
