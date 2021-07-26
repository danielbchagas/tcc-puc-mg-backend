using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarClientePorIdQuery : IBuscarClientePorIdQuery
    {
        public BuscarClientePorIdQuery(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<Cliente> Buscar(Guid id)
        {
            return await _repository.Buscar(id);
        }
    }
}
