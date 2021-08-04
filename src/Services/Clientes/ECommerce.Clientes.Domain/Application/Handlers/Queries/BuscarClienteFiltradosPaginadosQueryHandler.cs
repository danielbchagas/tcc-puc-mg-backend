using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries
{
    public class BuscarClienteFiltradosPaginadosQueryHandler : IRequestHandler<BuscarClientesFiltradosPaginadosQuery, IEnumerable<Cliente>>
    {
        public BuscarClienteFiltradosPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async System.Threading.Tasks.Task<IEnumerable<Cliente>> Handle(BuscarClientesFiltradosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Filtro, request.Pagina, request.Linhas);
        }
    }
}
