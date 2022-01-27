using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carts.Application.Queries;
using ECommerce.Carts.Domain.Interfaces.Repositories;
using ECommerce.Carts.Domain.Models;
using MediatR;

namespace ECommerce.Carts.Application.Handlers.Queries
{
    public class GetCartByCustomerQueryHandler : IRequestHandler<GetCartByCustomerQuery, Cart>
    {
        private readonly ICartRepository _repository;

        public GetCartByCustomerQueryHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cart> Handle(GetCartByCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Get(request.CustomerId);

            return await Task.FromResult(result);
        }
    }
}
