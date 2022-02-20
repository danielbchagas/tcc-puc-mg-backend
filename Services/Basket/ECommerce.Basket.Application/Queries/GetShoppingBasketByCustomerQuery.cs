using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Queries
{
    public class GetShoppingBasketByCustomerQuery : IRequest<ShoppingBasket>
    {
        public GetShoppingBasketByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
