using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

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
