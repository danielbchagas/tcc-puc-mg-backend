using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Queries
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, Document>
    {
        public GetDocumentQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        private readonly IDocumentRepository _repository;

        public async Task<Document> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
