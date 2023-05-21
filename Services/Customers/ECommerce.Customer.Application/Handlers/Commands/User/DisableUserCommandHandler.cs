using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.User
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, ValidationResult>
    {
        public DisableUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(DisableUserCommand request, CancellationToken cancellationToken)
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
