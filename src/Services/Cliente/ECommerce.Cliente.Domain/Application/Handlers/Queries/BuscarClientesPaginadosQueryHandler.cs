using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
{
    public class BuscarClientesPaginadosQueryHandler : IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Models.Cliente>>
    {
        public BuscarClientesPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<IEnumerable<Models.Cliente>> Handle(BuscarClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
