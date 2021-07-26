using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    public interface IClienteQuery
    {
        Task<Cliente> Buscar(Guid id);
        Task<IEnumerable<Cliente>> Buscar(int? pagina, int? linhas);
        Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int? pagina, int? linhas);
    }
}
