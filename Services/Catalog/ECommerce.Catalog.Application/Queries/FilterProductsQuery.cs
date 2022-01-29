using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ECommerce.Catalog.Domain.Models;
using MediatR;

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
