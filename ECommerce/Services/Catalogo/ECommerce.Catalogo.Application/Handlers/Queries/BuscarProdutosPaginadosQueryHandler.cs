using ECommerce.Catalogo.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Application.Handlers.Queries
{
    public class BuscarProdutosPaginadosQueryHandler : IRequestHandler<BuscarProdutosPaginadosQuery, IEnumerable<Produto>>
    {
        public BuscarProdutosPaginadosQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<IEnumerable<Produto>> Handle(BuscarProdutosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Pagina, request.Linhas);
        }
    }
}
