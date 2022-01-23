using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class GetTelefoneQueryHandler : IRequestHandler<GetTelefoneQuery, Telefone>
    {
        public GetTelefoneQueryHandler(ITelefoneRepository repository)
        {
            _repository = repository;
        }

        private readonly ITelefoneRepository _repository;

        public async Task<Telefone> Handle(GetTelefoneQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
