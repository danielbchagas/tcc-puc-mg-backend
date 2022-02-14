using ECommerce.Basket.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Queries
{
    public class GetBasketItemQuery : IRequest<BasketItem>
    {
        public GetBasketItemQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
