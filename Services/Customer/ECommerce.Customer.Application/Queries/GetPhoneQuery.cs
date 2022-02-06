using ECommerce.Core.Models.Customer;
using MediatR;
using System;

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
