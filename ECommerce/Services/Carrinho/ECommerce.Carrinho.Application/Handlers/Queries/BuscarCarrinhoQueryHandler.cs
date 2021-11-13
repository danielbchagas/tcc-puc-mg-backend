using ECommerce.Carrinho.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Application.Handlers.Queries
{
    public class BuscarCarrinhoQueryHandler : IRequestHandler<BuscarCarrinhoQuery, CarrinhoCliente>
    {
        public Task<CarrinhoCliente> Handle(BuscarCarrinhoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
