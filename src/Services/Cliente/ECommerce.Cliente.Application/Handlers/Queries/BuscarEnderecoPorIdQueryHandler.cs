using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class BuscarEnderecoPorIdQueryHandler : IRequestHandler<BuscarEnderecoPorIdQuery, Endereco>
    {
        public BuscarEnderecoPorIdQueryHandler(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        private readonly IEnderecoRepository _repository;

        public async Task<Endereco> Handle(BuscarEnderecoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
