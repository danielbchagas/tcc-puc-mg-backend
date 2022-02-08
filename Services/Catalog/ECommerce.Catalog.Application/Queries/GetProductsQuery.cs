using ECommerce.Core.Models.Catalog;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
