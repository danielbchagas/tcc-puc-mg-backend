using ECommerce.Clientes.Domain.Application.Queries.Endereco;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Endereco
{
    public class BuscarEnderecoPorIdQueryHandler : IRequestHandler<BuscarEnderecoPorIdQuery, Dominio.Endereco>
    {
        public BuscarEnderecoPorIdQueryHandler(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        private readonly IEnderecoRepository _repository;

        public async Task<Dominio.Endereco> Handle(BuscarEnderecoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
