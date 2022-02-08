using ECommerce.Core.Models.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Catalog.Application.Queries
{
    public class FilterProductsQuery : IRequest<IEnumerable<Product>>
    {
        public FilterProductsQuery(Expression<Func<Product, bool>> filter)
        {
            Filter = filter;
        }

        public Expression<Func<Product, bool>> Filter { get; set; }
    }
}
