using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    public interface IBuscarEnderecosFiltradosPaginadosQuery
    {
        Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int? pagina, int? linhas);
    }
}
