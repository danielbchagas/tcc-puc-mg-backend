using System;
using ECommerce.Catalog.Domain.Models;
using MediatR;

namespace ECommerce.Catalog.Application.Queries
{
    public class GetProductQuery : IRequest<Product>
    {
        public GetProductQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
