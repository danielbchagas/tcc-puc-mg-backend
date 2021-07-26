using ECommerce.Produtos.Domain.Interfaces.Queries;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutoPorIdQuery : IBuscarProdutoPorIdQuery
    {
        public BuscarProdutoPorIdQuery(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<Produto> Buscar(Guid id)
        {
            return await _repository.Buscar(id);
        }
    }
}
