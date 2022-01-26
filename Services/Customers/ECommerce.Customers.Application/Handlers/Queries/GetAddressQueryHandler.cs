using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Queries
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
