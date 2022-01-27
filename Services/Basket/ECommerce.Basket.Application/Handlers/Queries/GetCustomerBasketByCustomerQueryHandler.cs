using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetCustomerBasketByCustomerQueryHandler : IRequestHandler<GetCustomerBasketByCustomerQuery, CustomerBasket>
    {
        private readonly ICustomerBasketRepository _repository;

        public GetCustomerBasketByCustomerQueryHandler(ICustomerBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerBasket> Handle(GetCustomerBasketByCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(request.CustomerId);

            return await Task.FromResult(result);
        }
    }
}
