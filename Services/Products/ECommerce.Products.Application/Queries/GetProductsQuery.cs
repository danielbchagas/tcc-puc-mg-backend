using ECommerce.Products.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Products.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
