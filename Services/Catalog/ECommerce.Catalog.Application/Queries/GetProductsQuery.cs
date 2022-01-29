using ECommerce.Catalog.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
