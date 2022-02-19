using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetShoppingBasketQueryHandler : IRequestHandler<GetShoppingBasketQuery, ShoppingBasket>
    {
        public GetShoppingBasketQueryHandler(IShoppingBasketRepository repository)
        {
            _repository = repository;
        }

        private readonly IShoppingBasketRepository _repository;

        public async Task<ShoppingBasket> Handle(GetShoppingBasketQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
