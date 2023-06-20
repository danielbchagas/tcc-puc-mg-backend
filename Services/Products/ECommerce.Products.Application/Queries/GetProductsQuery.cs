using ECommerce.Products.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Products.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public GetProductsQuery(string query, int? page = null, int? pageSize = null)
        {
            Expression = q => q.Name.Contains(query);
            Page = page;
            PageSize = pageSize;
        }

        public Expression<Func<Product, bool>> Expression { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
