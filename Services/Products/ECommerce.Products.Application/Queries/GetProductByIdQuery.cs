using System;
using ECommerce.Products.Domain.Models;
using MediatR;

namespace ECommerce.Products.Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
