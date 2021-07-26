using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    class BuscarClientePorIdQuery : IBuscarClientePorIdQuery
    {
        public Task<Cliente> Buscar(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
