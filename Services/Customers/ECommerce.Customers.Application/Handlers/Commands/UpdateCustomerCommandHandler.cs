using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ValidationResult>
    {
        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerRepository _repository;

        public async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Enabled = request.Enabled;

            var validation = customer.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(customer);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
