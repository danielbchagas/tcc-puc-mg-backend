using ECommerce.Customers.Application.Commands.User;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.User
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ValidationResult>
    {
        public DeleteCustomerCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ValidationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            await _repository.Delete(request.Id);
            await _repository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
