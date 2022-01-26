using System.Threading;
using System.Threading.Tasks;
using ECommerce.Catalog.Application.Queries;
using ECommerce.Catalog.Domain.Interfaces.Repositories;
using ECommerce.Catalog.Domain.Models;
using MediatR;

namespace ECommerce.Catalog.Application.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        private readonly IProductRepository _repository;

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
