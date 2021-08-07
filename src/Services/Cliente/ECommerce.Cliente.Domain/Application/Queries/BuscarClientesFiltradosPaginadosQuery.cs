using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Queries
{
    public class BuscarClientesFiltradosPaginadosQuery : IRequest<IEnumerable<Models.Cliente>>
    {
        public BuscarClientesFiltradosPaginadosQuery(Expression<Func<Models.Cliente, bool>> filtro, int pagina, int linhas)
        {
            Filtro = filtro;
            Pagina = pagina;
            Linhas = linhas;
        }

        public Expression<Func<Models.Cliente, bool>> Filtro { get; set; }
        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
