using System;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Queries
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
