using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Queries
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllUsersQuery, IList<Customer>>
    {
        public GetAllCustomerQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<IList<Customer>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get();
        }
    }
}
