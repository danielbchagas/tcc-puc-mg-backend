using ECommerce.Pedido.Application.Queries;
using ECommerce.Pedido.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PedidoCliente = ECommerce.Pedido.Domain.Models.Pedido;

namespace ECommerce.Pedido.Application.Handlers.Queries
{
    public class BuscarPedidoPorIdHandler : IRequestHandler<BuscarPedidoPorId, PedidoCliente>
    {
        private readonly IPedidoRepository _repository;

        public BuscarPedidoPorIdHandler(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PedidoCliente> Handle(BuscarPedidoPorId request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
