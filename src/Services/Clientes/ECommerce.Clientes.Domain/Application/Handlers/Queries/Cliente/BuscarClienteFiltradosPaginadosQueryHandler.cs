using ECommerce.Clientes.Domain.Application.Queries.Cliente;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Cliente
{
    public class BuscarClienteFiltradosPaginadosQueryHandler : IRequestHandler<BuscarClientesFiltradosPaginadosQuery, IEnumerable<Dominio.Cliente>>
    {
        public BuscarClienteFiltradosPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async System.Threading.Tasks.Task<IEnumerable<Dominio.Cliente>> Handle(BuscarClientesFiltradosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Filtro, request.Pagina, request.Linhas);
        }
    }
}
