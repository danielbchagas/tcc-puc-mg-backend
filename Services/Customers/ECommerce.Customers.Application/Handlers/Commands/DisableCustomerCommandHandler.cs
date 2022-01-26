using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Commands
{
    public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, ValidationResult>
    {
        public DisableCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerRepository _repository;
        
        public async Task<ValidationResult> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);

            if (customer != null)
            {
                var validation = customer.Validate();

                if (validation.IsValid)
                {
                    customer.Enabled = false;

                    await _repository.UnitOfWork.Commit();
                }

                return await Task.FromResult(validation);
            }

            return await Task.FromResult(new ValidationResult());
        }
    }
}
