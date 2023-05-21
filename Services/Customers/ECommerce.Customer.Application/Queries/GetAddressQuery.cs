using ECommerce.Customers.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Queries
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
