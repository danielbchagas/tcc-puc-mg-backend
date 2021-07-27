using ECommerce.Produtos.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutosFiltradosPaginadosQuery : IRequest<IEnumerable<Produto>>
    {
        public BuscarProdutosFiltradosPaginadosQuery(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas)
        {
            Filtro = filtro;
            Pagina = pagina;
            Linhas = linhas;
        }

        public Expression<Func<Produto, bool>> Filtro { get; set; }
        public int? Pagina { get; set; }
        public int? Linhas { get; set; }
    }
}
