using ECommerce.Core.Models.Customer;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Application.Handlers.Queries
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, Address>
    {
        public GetAddressQueryHandler(IAddressRepository repository)
        {
            _repository = repository;
        }

        private readonly IAddressRepository _repository;

        public async Task<Address> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
