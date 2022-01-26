using System;
using ECommerce.Customers.Domain.Models;
using MediatR;

namespace ECommerce.Customers.Application.Queries
{
    public class GetPhoneQuery : IRequest<Phone>
    {
        public GetPhoneQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
