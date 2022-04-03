using ECommerce.Basket.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Basket.Application.Queries
{
    public class GetShoppingBasketQuery : IRequest<IList<ShoppingBasket>>
    {
        public GetShoppingBasketQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
        public Guid CustomerId { get; set; }
    }
}
