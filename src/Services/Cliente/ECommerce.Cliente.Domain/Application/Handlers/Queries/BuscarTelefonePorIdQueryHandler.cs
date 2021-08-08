using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
{
    public class BuscarTelefonePorIdQueryHandler : IRequestHandler<BuscarTelefonePorIdQuery, Telefone>
    {
        public BuscarTelefonePorIdQueryHandler(ITelefoneRepository repository)
        {
            _repository = repository;
        }

        private readonly ITelefoneRepository _repository;

        public async Task<Telefone> Handle(BuscarTelefonePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
