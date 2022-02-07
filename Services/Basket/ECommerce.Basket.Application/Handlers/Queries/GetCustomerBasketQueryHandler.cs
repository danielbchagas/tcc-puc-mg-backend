using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Core.Models.Basket;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetCustomerBasketQueryHandler : IRequestHandler<GetCustomerBasketQuery, CustomerBasket>
    {
        public GetCustomerBasketQueryHandler(ICustomerBasketRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerBasketRepository _repository;

        public async Task<CustomerBasket> Handle(GetCustomerBasketQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
