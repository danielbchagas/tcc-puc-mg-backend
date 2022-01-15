using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;

namespace ECommerce.Cliente.Application.Queries
{
    public class BuscarClientesFiltradosPaginadosQuery : IRequest<IEnumerable<Domain.Models.Cliente>>
    {
        public BuscarClientesFiltradosPaginadosQuery(Expression<Func<Domain.Models.Cliente, bool>> filtro, int pagina, int linhas)
        {
            Filtro = filtro;
            Pagina = pagina;
            Linhas = linhas;
        }

        public Expression<Func<Domain.Models.Cliente, bool>> Filtro { get; set; }
        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
