using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class BuscarClientesPaginadosQueryHandler : IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Domain.Models.Cliente>>
    {
        public BuscarClientesPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<IEnumerable<Domain.Models.Cliente>> Handle(BuscarClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
