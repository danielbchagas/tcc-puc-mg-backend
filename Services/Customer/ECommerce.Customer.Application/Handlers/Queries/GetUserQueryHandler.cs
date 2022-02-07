using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        public GetUserQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }
    }
}
