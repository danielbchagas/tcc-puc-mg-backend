using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Queries
{
    public class GetEmailQueryHandler : IRequestHandler<GetEmailQuery, Email>
    {
        public GetEmailQueryHandler(IEmailRepository repository)
        {
            _repository = repository;
        }

        private readonly IEmailRepository _repository;

        public async Task<Email> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
