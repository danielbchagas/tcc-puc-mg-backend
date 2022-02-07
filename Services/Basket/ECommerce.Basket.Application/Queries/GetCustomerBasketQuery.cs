using ECommerce.Core.Models.Basket;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Queries
{
    public class GetCustomerBasketQuery : IRequest<CustomerBasket>
    {
        public GetCustomerBasketQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
