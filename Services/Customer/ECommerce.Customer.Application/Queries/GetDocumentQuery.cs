using System;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Queries
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
