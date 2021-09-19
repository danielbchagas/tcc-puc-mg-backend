using ECommerce.Catalogo.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Application.Handlers.Queries
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
