using ECommerce.Core.Models.Ordering;
using MediatR;
using System;

namespace ECommerce.Ordering.Application.Queries
{
    public class GetPedidoQuery : IRequest<Order>
    {
        public GetPedidoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
