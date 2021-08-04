using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries
{
    public class BuscarClientesPorIdQueryHandler : IRequestHandler<BuscarClientePorIdQuery, Cliente>
    {
        public BuscarClientesPorIdQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<Cliente> Handle(BuscarClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
