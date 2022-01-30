using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ValidationResult>
    {
        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
