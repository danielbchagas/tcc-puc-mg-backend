using ECommerce.Produtos.Domain.Interfaces.Queries;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutoQuery : IProdutoQuery
    {
        public BuscarProdutoQuery(IProdutoRepository repository)
        {
            _produtoRepository = repository;
        }

        private readonly IProdutoRepository _produtoRepository;

        public async Task<Produto> Buscar(Guid id)
        {
            return await _produtoRepository.Buscar(id);
        }

        public async Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas)
        {
            return await _produtoRepository.Buscar(pagina, linhas);
        }

        public async Task<IEnumerable<Produto>> Buscar(Expression<Func<Produto, bool>> filtro, int? pagina, int? linhas)
        {
            return await _produtoRepository.Buscar(filtro, pagina, linhas);
        }
    }
}
