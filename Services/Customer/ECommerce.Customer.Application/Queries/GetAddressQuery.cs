using ECommerce.Core.Models.Customer;
using MediatR;
using System;

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
