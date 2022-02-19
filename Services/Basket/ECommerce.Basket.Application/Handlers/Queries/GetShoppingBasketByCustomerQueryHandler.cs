using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetShoppingBasketByCustomerQueryHandler : IRequestHandler<GetShoppingBasketByCustomerQuery, ShoppingBasket>
    {
        private readonly IShoppingBasketRepository _repository;

        public GetShoppingBasketByCustomerQueryHandler(IShoppingBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingBasket> Handle(GetShoppingBasketByCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByCustomer(request.CustomerId);

            return await Task.FromResult(result);
        }
    }
}
