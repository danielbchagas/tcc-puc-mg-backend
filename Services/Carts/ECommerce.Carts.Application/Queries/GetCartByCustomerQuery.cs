using System;
using ECommerce.Carts.Domain.Models;
using MediatR;

namespace ECommerce.Carts.Application.Queries
{
    public class GetCartByCustomerQuery : IRequest<Cart>
    {
        public GetCartByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
