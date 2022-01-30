using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ValidationResult>
    {
        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            await _repository.Delete(request.Id);
            var success = await _repository.UnitOfWork.Commit();

            if(!success)
                validation.Errors.Add(new ValidationFailure("", "Customer can not be deleted."));

            return await Task.FromResult(validation);
        }
    }
}
