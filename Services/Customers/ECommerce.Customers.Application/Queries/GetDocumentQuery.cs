using System;
using ECommerce.Customers.Domain.Models;
using MediatR;

namespace ECommerce.Customers.Application.Queries
{
    public class GetDocumentQuery : IRequest<Document>
    {
        public GetDocumentQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
