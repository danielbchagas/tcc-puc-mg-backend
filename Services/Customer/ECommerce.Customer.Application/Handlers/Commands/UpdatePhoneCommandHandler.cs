using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
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
