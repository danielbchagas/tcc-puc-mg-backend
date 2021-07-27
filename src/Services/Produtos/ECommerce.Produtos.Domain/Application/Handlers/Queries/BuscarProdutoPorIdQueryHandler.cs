using ECommerce.Produtos.Domain.Application.Queries;
using ECommerce.Produtos.Domain.Interfaces.Repositories;
using ECommerce.Produtos.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Handlers.Queries
{
    public class BuscarProdutoPorIdQueryHandler : IRequestHandler<BuscarProdutoPorIdQuery, Produto>
    {
        public BuscarProdutoPorIdQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<Produto> Handle(BuscarProdutoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
