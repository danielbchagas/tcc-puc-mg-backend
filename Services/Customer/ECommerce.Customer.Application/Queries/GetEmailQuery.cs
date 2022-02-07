using ECommerce.Core.Models.Customer;
using MediatR;
using System;

namespace ECommerce.Customer.Application.Queries
{
    public class GetEmailQuery : IRequest<Email>
    {
        public GetEmailQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
