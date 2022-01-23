using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Catalogo.Application.Queries
{
    public class FilterProdutosQuery : IRequest<IEnumerable<Produto>>
    {
        public FilterProdutosQuery(Expression<Func<Produto, bool>> filtro, int pagina, int linhas)
        {
            Filtro = filtro;
            Pagina = pagina;
            Linhas = linhas;
        }

        public Expression<Func<Produto, bool>> Filtro { get; set; }
        public int Pagina { get; set; }
        public int Linhas { get; set; }
    }
}
