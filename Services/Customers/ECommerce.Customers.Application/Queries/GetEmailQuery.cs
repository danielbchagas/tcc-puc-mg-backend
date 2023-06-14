using ECommerce.Customers.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Queries
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
