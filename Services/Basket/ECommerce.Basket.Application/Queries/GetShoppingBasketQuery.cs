using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Queries
{
    public class GetShoppingBasketQuery : IRequest<ShoppingBasket>
    {
        public GetShoppingBasketQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
