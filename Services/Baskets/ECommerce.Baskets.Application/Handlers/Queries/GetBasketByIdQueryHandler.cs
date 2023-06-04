using ECommerce.Baskets.Application.Queries;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Application.Handlers.Queries
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, Domain.Models.Basket>
    {
        private readonly IBasketRepository _repository;

        public GetBasketByIdQueryHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Models.Basket> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(request.Id);

            return await Task.FromResult(result);
        }
    }
}
