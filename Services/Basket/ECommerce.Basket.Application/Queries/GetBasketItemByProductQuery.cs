using ECommerce.Basket.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Queries
{
    public class GetBasketItemByProductQuery : IRequest<BasketItem>
    {
        public GetBasketItemByProductQuery(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; set; }
    }
}
