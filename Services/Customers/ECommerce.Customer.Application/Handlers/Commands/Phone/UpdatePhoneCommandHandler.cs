using ECommerce.Customers.Application.Commands.Phone;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Phone
{
    public class UpdatePhoneCommandHandler : IRequestHandler<UpdatePhoneCommand, ValidationResult>
    {
        public UpdatePhoneCommandHandler(IPhoneRepository repository)
        {
            _repository = repository;
        }

        private readonly IPhoneRepository _repository;

        public async Task<ValidationResult> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
        {
            var phone = await _repository.Get(request.Id);
            phone.Number = request.Number;

            var validation = phone.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(phone);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
