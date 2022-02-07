using ECommerce.Core.Models.Ordering;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Application.Handlers.Queries
{
    public class GetPedidoQueryHandler : IRequestHandler<GetPedidoQuery, Order>
    {
        private readonly IPedidoRepository _repository;

        public GetPedidoQueryHandler(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.Get(request.Id);
            pedido.Totalize();

            return pedido;
        }
    }
}
