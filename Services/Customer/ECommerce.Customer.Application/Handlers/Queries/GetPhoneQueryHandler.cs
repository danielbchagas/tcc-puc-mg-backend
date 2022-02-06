using ECommerce.Core.Models.Customer;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Application.Handlers.Queries
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
