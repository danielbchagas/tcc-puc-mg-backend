using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class BuscarClientesPorIdQueryHandler : IRequestHandler<BuscarClientePorIdQuery, Domain.Models.Cliente>
    {
        public BuscarClientesPorIdQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<Domain.Models.Cliente> Handle(BuscarClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
