using System;
using ECommerce.Products.Domain.Models;
using MediatR;

namespace ECommerce.Products.Application.Queries
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
