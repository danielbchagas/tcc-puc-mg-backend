using ECommerce.Clientes.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    public interface IBuscarEnderecoPorIdQuery
    {
        Task<Cliente> Buscar(Guid id);
    }
}
