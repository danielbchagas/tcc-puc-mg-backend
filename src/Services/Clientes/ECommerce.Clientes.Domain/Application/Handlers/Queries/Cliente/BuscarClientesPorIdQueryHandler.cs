using ECommerce.Clientes.Domain.Application.Queries.Cliente;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Cliente
{
    public class BuscarClientesPorIdQueryHandler : IRequestHandler<BuscarClientePorIdQuery, Dominio.Cliente>
    {
        public BuscarClientesPorIdQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<Dominio.Cliente> Handle(BuscarClientePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
