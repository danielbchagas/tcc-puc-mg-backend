﻿using ECommerce.Clientes.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Interfaces.Queries
{
    public interface IBuscarClientesPaginadosQuery
    {
        public Task<IEnumerable<Cliente>> Buscar(int? pagina, int? linhas);
    }
}
