using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ValidationResult>
    {
        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;
        
        public async Task<ValidationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var customer = new User(id: request.Id, firstName: request.FirstName, lastName: request.LastName, enabled: request.Enabled, document: request.Document, email: request.Email, phone: request.Phone);

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(customer);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
