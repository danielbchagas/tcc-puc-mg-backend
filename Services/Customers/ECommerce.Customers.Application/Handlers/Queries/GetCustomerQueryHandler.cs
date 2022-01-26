using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Queries;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        public GetCustomerQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerRepository _repository;

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
