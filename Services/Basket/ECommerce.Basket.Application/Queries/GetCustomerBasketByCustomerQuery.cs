using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

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
