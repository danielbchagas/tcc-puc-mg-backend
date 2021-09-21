using MediatR;
using System;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.Carrinho;

namespace ECommerce.Carrinho.Application.Queries
{
    public class BuscarCarrinhoQuery : IRequest<CarrinhoCliente>
    {
        public BuscarCarrinhoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
