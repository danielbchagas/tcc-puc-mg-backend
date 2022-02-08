using ECommerce.Core.Models.Ordering;
using MediatR;
using System;

namespace ECommerce.Ordering.Application.Queries
{
    public class GetOrderQuery : IRequest<Order>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
