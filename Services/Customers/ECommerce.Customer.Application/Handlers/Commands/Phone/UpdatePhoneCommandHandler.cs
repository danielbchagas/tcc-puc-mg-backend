using ECommerce.Customers.Application.Commands.Phone;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Phone
{
    public class UpdatePhoneCommandHandler : IRequestHandler<UpdatePhoneCommand, ValidationResult>
    {
        public UpdatePhoneCommandHandler(IPhoneRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly IPhoneRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ValidationResult> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
        {
            var phone = await _repository.Get(request.Id);
            phone.Number = request.Number;

            var validation = phone.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(phone);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
