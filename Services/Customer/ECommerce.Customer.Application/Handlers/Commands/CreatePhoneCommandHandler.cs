using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customer.Application.Commands;
using ECommerce.Customer.Domain.Interfaces.Repositories;
using ECommerce.Customer.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Handlers.Commands
{
    public class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, ValidationResult>
    {
        public CreatePhoneCommandHandler(IPhoneRepository repository, IMediator mediator)
        {
            _repository = repository;
        }

        private readonly IPhoneRepository _repository;
        
        public async Task<ValidationResult> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var phones = await _repository.Get(t => t.UserId == request.UserId);

            if(phones.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um telefone cadastrado."));

                if (phones.Any(t => t.Number == request.Number))
                    validation.Errors.Add(new ValidationFailure("", "O telefone informado já está em uso."));

                return validation;
            }

            var phone = new Phone(request.Number, request.UserId);

            validation = phone.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(phone);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
