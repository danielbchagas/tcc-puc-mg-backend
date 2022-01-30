using System;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Queries
{
    public class GetAddressQuery : IRequest<Address>
    {
        public GetAddressQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
