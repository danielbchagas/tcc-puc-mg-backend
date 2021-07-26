using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    class BuscarClienteQuery : IClienteQuery
    {
        public Task<Cliente> Buscar(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cliente>> Buscar(int? pagina, int? linhas)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int? pagina, int? linhas)
        {
            throw new NotImplementedException();
        }
    }
}
