using ECommerce.Core.Models.Catalog;
using MediatR;
using System;

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
