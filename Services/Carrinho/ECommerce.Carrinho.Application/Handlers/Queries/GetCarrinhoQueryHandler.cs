using ECommerce.Carrinho.Application.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.CarrinhoCompras;

namespace ECommerce.Carrinho.Application.Handlers.Queries
{
    public class GetCarrinhoQueryHandler : IRequestHandler<GetCarrinhoQuery, CarrinhoCliente>
    {
        public Task<CarrinhoCliente> Handle(GetCarrinhoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
