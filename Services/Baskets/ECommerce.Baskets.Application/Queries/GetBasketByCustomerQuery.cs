using System;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Queries
{
    public class GetBasketByCustomerQuery : IRequest<Domain.Models.Basket>
    {
        public GetBasketByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
