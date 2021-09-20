using ECommerce.Carrinho.Application.Queries;
using ECommerce.Carrinho.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Queries
{
    public class BuscarPorProdutoIdQueryHandler : IRequestHandler<BuscarPorProdutoIdQuery, ItemCarrinho>
    {
        public Task<ItemCarrinho> Handle(BuscarPorProdutoIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
