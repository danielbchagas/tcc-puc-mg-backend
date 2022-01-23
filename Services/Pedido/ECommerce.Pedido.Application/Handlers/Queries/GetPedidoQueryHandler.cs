using ECommerce.Pedido.Application.Queries;
using ECommerce.Pedido.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Application.Handlers.Queries
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
            pedido.CalcularTotalPedido();

            return pedido;
        }
    }
}
