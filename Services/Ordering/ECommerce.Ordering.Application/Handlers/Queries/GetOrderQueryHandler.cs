using ECommerce.Core.Models.Ordering;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Application.Handlers.Queries
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _repository;

        public GetOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.Get(request.Id);
            pedido.Totalize();

            return pedido;
        }
    }
}
