using ECommerce.Products.Application.Queries;
using ECommerce.Products.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Products.Application.Handlers.Queries
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, (Guid, string)>
    {
        private readonly IProductRepository _productRepository;

        public GetImageQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(Guid, string)> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetImage(request.ProductId);
        }
    }
}
