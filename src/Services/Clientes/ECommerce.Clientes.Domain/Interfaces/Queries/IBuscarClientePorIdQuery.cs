using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    public interface IBuscarClientePorIdQuery
    {
        Task<Cliente> Buscar(Guid id);
    }
}
