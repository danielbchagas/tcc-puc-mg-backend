using ECommerce.Cliente.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Queries
{
    public class GetDocumentoQueryHandler : IRequestHandler<GetDocumentoQuery, Documento>
    {
        public GetDocumentoQueryHandler(IDocumentoRepository repository)
        {
            _repository = repository;
        }

        private readonly IDocumentoRepository _repository;

        public async Task<Documento> Handle(GetDocumentoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Buscar(request.Id);
        }
    }
}
