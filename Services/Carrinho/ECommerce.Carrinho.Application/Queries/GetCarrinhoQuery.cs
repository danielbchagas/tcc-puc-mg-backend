using MediatR;
using System;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.CarrinhoCompras;

namespace ECommerce.Carrinho.Application.Queries
{
    public class GetCarrinhoQuery : IRequest<CarrinhoCliente>
    {
        public GetCarrinhoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
