using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Application.Queries.Cliente;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries.Cliente
{
    public class BuscarClientesPaginadosQueryHandler : IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Dominio.Cliente>>
    {
        public BuscarClientesPaginadosQueryHandler(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<IEnumerable<Dominio.Cliente>> Handle(BuscarClientesPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
