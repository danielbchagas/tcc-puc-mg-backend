using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Catalog.Application.Queries;
using ECommerce.Catalog.Domain.Interfaces.Repositories;
using ECommerce.Catalog.Domain.Models;
using MediatR;

namespace ECommerce.Catalog.Application.Handlers.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        private readonly IProductRepository _repository;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Page, request.Rows);
        }
    }
}
