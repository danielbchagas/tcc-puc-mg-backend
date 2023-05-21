using ECommerce.Customers.Application.Commands.Email;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Email
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, ValidationResult>
    {
        public UpdateEmailCommandHandler(IEmailRepository repository)
        {
            _repository = repository;
        }

        private readonly IEmailRepository _repository;

        public async Task<ValidationResult> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var email = await _repository.Get(request.Id);
            email.Address = request.Address;

            var validation = email.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(email);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
