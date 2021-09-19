using ECommerce.Catalogo.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Application.Handlers.Queries
{
    public class BuscarProdutosFiltradosPaginadosQueryHandler : IRequestHandler<BuscarProdutosFiltradosPaginadosQuery, IEnumerable<Produto>>
    {
        public BuscarProdutosFiltradosPaginadosQueryHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        private readonly IProdutoRepository _repository;

        public async Task<IEnumerable<Produto>> Handle(BuscarProdutosFiltradosPaginadosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Filtro, request.Pagina, request.Linhas);
        }
    }
}
