using System.Threading;
using System.Threading.Tasks;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using MediatR;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

namespace ECommerce.Ordering.Application.Handlers.Queries
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, PedidoCliente>
    {
        private readonly IOrderRepository _repository;

        public GetOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PedidoCliente> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.Buscar(request.Id);
            pedido.Totalize();

            return pedido;
        }
    }
}
