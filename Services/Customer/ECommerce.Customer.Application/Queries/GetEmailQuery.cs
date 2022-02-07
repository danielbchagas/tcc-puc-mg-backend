using System;
using ECommerce.Customer.Domain.Models;
using MediatR;

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
