using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerRepository _repository;
        
        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(id: request.Id, firstName: request.FirstName, lastName: request.LastName, enabled: request.Enabled, document: request.Document, email: request.Email, phone: request.Phone);

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
