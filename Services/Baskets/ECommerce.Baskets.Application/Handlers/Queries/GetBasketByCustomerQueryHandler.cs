using ECommerce.Basket.Application.Queries;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Basket.Application.Handlers.Queries
{
    public class GetBasketByCustomerQueryHandler : IRequestHandler<GetBasketByCustomerQuery, Domain.Models.Basket>
    {
        private readonly IBasketRepository _repository;

        public GetBasketByCustomerQueryHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Models.Basket> Handle(GetBasketByCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByCustomer(request.CustomerId);

            return await Task.FromResult(result);
        }
    }
}
