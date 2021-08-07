using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Queries
{
    public class BuscarDocumentoPorIdQueryHandler : IRequestHandler<BuscarDocumentoPorIdQuery, Documento>
    {
        public BuscarDocumentoPorIdQueryHandler(IDocumentoRepository repository)
        {
            _repository = repository;
        }

        private readonly IDocumentoRepository _repository;

        public async Task<Documento> Handle(BuscarDocumentoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
