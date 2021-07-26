using ECommerce.Clientes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    interface IBuscarClientesFiltradosPaginadosQuery
    {
        public Task<IEnumerable<Cliente>> Buscar(int? pagina, int? linhas);
    }
}
