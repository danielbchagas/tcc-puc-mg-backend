using ECommerce.Core.Models.Basket;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Queries
{
    public class GetCustomerBasketByCustomerQuery : IRequest<CustomerBasket>
    {
        public GetCustomerBasketByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
