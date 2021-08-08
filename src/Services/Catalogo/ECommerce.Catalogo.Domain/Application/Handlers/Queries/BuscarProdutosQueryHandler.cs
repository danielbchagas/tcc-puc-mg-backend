using ECommerce.Catalogo.Domain.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Domain.Application.Handlers.Queries
{
    public class BuscarProdutosQueryHandler : IRequestHandler<BuscarProdutosQuery, IEnumerable<Produto>>
    {
        public BuscarProdutosQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<IEnumerable<Produto>> Handle(BuscarProdutosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar();
        }
    }
}
