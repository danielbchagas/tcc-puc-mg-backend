using System;
using MediatR;

namespace ECommerce.Baskets.Application.Queries
{
    public class GetBasketByIdQuery : IRequest<Domain.Models.Basket>
    {
        public GetBasketByIdQuery(Guid customerId)
        {
            Id = customerId;
        }

        public Guid Id { get; set; }
    }
}
