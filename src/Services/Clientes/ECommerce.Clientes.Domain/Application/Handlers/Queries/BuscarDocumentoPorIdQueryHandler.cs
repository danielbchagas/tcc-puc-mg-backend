using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Queries
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
