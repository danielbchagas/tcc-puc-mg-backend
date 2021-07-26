using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarClientesFiltradosPaginadosQuery : IBuscarClientesFiltradosPaginadosQuery
    {
        public BuscarClientesFiltradosPaginadosQuery(IClienteRepository repository)
        {
            _repository = repository;
        }

        private readonly IClienteRepository _repository;

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> filtro, int? pagina, int? linhas)
        {
            return await _repository.Buscar(filtro, pagina, linhas);
        }
    }
}
