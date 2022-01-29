using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using MediatR;

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
