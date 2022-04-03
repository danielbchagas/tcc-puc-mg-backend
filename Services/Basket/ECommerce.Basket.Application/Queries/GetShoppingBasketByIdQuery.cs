using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Queries
{
    public class GetShoppingBasketByIdQuery : IRequest<ShoppingBasket>
    {
        public GetShoppingBasketByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
