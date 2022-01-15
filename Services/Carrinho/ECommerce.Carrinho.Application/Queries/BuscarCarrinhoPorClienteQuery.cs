using MediatR;
using System;
using CarrinhoCliente = ECommerce.Carrinho.Domain.Models.CarrinhoCompras;

namespace ECommerce.Carrinho.Application.Queries
{
    public class BuscarCarrinhoPorClienteQuery : IRequest<CarrinhoCliente>
    {
        public BuscarCarrinhoPorClienteQuery(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; set; }
    }
}
