using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries
{
    public class BuscarClientesPaginadosQueryHandler : IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Cliente>>
    {
        public BuscarClientesPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<IEnumerable<Cliente>> Handle(BuscarClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
