using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ValidationResult>
    {
        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        private readonly ICustomerRepository _repository;

        public async Task<ValidationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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
