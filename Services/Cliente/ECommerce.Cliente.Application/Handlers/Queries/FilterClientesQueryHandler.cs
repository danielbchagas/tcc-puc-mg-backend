using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class FilterClientesQueryHandler : IRequestHandler<FilterClientesQuery, IEnumerable<Domain.Models.Cliente>>
    {
        public FilterClientesQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async System.Threading.Tasks.Task<IEnumerable<Domain.Models.Cliente>> Handle(FilterClientesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Filtro, request.Pagina, request.Linhas);
        }
    }
}
