using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetShoppingBasketQueryHandler : IRequestHandler<GetShoppingBasketQuery, IList<ShoppingBasket>>
    {
        public GetShoppingBasketQueryHandler(IShoppingBasketRepository repository)
        {
            _repository = repository;
        }

        private readonly IShoppingBasketRepository _repository;

        public async Task<IList<ShoppingBasket>> Handle(GetShoppingBasketQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Filter(f => f.CustomerId == request.CustomerId);
        }
    }
}
