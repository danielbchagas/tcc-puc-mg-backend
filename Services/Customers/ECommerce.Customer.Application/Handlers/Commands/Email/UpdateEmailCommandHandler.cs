using ECommerce.Customers.Application.Commands.Email;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Email
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, ValidationResult>
    {
        public UpdateEmailCommandHandler(IEmailRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly IEmailRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ValidationResult> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            var email = await _repository.Get(request.Id);
            email.Address = request.Address;

            var validation = email.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(email);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
