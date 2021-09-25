using MediatR;
using System;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Application.Queries
{
    public class BuscarPedidoPorIdQuery : IRequest<PedidoCliente>
    {
        public BuscarPedidoPorIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
