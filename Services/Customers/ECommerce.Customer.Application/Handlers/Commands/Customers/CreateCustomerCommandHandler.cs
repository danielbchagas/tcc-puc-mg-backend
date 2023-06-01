using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.User
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        public CreateCustomerCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Models.Customer(
                id: request.Id, 
                firstName: request.FirstName, 
                lastName: request.LastName, 
                createdAt: request.CreatedAt, 
                document: request.Document, 
                email: request.Email, 
                phone: request.Phone);

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
