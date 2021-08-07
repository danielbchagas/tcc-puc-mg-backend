using System.Collections.Generic;
using System.Threading;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
{
    public class BuscarClienteFiltradosPaginadosQueryHandler : IRequestHandler<BuscarClientesFiltradosPaginadosQuery, IEnumerable<Models.Cliente>>
    {
        public BuscarClienteFiltradosPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async System.Threading.Tasks.Task<IEnumerable<Models.Cliente>> Handle(BuscarClientesFiltradosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Filtro, request.Pagina, request.Linhas);
        }
    }
}
