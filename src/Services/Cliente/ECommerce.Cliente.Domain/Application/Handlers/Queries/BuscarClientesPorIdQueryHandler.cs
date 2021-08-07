using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
{
    public class BuscarClientesPorIdQueryHandler : IRequestHandler<BuscarClientePorIdQuery, Models.Cliente>
    {
        public BuscarClientesPorIdQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<Models.Cliente> Handle(BuscarClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
