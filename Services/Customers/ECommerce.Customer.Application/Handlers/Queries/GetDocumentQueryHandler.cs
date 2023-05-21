using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Queries
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
