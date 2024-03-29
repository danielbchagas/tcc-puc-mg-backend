﻿using ECommerce.Customers.Domain.Models;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Queries
{
    public class GetUserQuery : IRequest<Customer>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
