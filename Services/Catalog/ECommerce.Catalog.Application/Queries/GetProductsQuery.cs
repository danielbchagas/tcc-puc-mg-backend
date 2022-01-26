using System.Collections.Generic;
using ECommerce.Catalog.Domain.Models;
using MediatR;

namespace ECommerce.Catalog.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public GetProductsQuery(int page, int rows)
        {
            Page = page;
            Rows = rows;
        }

        public int Page { get; set; }
        public int Rows { get; set; }
    }
}
