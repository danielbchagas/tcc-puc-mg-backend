using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetBasketItemByProductQueryHandler : IRequestHandler<GetBasketItemByProductQuery, BasketItem>
    {
        private readonly IBasketItemRepository _repository;

        public GetBasketItemByProductQueryHandler(IBasketItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<BasketItem> Handle(GetBasketItemByProductQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByProductId(request.ProductId);
        }
    }
}
