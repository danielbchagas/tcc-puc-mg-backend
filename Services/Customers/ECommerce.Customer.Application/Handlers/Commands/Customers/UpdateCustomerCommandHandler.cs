using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.User
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ValidationResult>
    {
        public UpdateCustomerCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Get(request.Id);
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;

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
