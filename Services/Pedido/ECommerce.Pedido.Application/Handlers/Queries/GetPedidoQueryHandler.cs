using System.Threading;
using System.Threading.Tasks;
using ECommerce.Ordering.Application.Queries;
using ECommerce.Ordering.Domain.Interfaces.Repositories;
using MediatR;
using PedidoCliente = ECommerce.Ordering.Domain.Models.Order;

namespace ECommerce.Ordering.Application.Handlers.Queries
{
    public class GetPedidoQueryHandler : IRequestHandler<GetPedidoQuery, PedidoCliente>
    {
        private readonly IPedidoRepository _repository;

        public GetPedidoQueryHandler(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PedidoCliente> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _repository.Buscar(request.Id);
            pedido.Totalize();

            return pedido;
        }
    }
}
