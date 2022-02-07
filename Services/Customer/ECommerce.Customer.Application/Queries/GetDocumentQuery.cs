﻿using ECommerce.Core.Models.Customer;
using MediatR;
using System;

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
