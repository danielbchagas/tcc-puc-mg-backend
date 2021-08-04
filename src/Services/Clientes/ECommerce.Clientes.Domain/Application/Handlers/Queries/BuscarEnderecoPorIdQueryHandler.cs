using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries
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
