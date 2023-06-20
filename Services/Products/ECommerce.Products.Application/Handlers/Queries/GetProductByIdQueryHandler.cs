using System.Threading;
using System.Threading.Tasks;
using ECommerce.Products.Application.Queries;
using ECommerce.Products.Domain.Interfaces.Repositories;
using ECommerce.Products.Domain.Models;
using MediatR;

namespace ECommerce.Products.Application.Handlers.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        private readonly IProductRepository _repository;

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
