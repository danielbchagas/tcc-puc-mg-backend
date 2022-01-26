using System;
using ECommerce.Customers.Domain.Models;
using MediatR;

namespace ECommerce.Customers.Application.Queries
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public GetCustomerQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
