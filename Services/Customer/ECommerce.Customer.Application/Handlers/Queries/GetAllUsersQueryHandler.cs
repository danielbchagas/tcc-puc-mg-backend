using ECommerce.Customer.Application.Queries;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customer.Application.Handlers.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<User>>
    {
        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<IList<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get();
        }
    }
}
