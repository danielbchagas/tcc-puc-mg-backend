using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Queries
{
    public class GetPhoneQueryHandler : IRequestHandler<GetPhoneQuery, Phone>
    {
        public GetPhoneQueryHandler(IPhoneRepository repository)
        {
            _repository = repository;
        }

        private readonly IPhoneRepository _repository;

        public async Task<Phone> Handle(GetPhoneQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
