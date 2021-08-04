using ECommerce.Clientes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Clientes.Domain.Application.Queries
{
    public class BuscarClientesFiltradosPaginadosQuery : IRequest<IEnumerable<Cliente>>
    {
        public BuscarClientesFiltradosPaginadosQuery(Expression<Func<Cliente, bool>> filtro, int pagina, int linhas)
        {
            Filtro = filtro;
            Pagina = pagina;
            Linhas = linhas;
        }

        public Expression<Func<Cliente, bool>> Filtro { get; set; }
        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
