using ECommerce.Produtos.Domain.Interfaces.Queries;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutosFiltradosPaginadosQuery : IBuscarProdutosFiltradosPaginadosQuery
    {
        public BuscarProdutosFiltradosPaginadosQuery(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas)
        {
            return await _repository.Buscar(filtro, pagina, linhas);
        }
    }
}
