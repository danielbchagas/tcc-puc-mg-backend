using ECommerce.Produtos.Domain.Interfaces.Queries;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Queries
{
    public class BuscarProdutosPaginadosQuery : IBuscarProdutosPaginadosQuery
    {
        public BuscarProdutosPaginadosQuery(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<IEnumerable<Produto>> Buscar(int? pagina, int? linhas)
        {
            return await _repository.Buscar(pagina, linhas);
        }
    }
}
