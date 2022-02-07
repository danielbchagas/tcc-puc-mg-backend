using System;
using MediatR;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

namespace ECommerce.Ordering.Application.Queries
{
    public class GetPedidoQuery : IRequest<PedidoCliente>
    {
        public GetPedidoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
