using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetBasketItemQueryHandler : IRequestHandler<GetBasketItemQuery, BasketItem>
    {
        private readonly IBasketItemRepository _repository;

        public GetBasketItemQueryHandler(IBasketItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<BasketItem> Handle(GetBasketItemQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
