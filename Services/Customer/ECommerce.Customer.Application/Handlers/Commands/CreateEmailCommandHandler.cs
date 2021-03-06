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
    public class CreateEmailCommandHandler : IRequestHandler<CreateEmailCommand, ValidationResult>
    {
        public CreateEmailCommandHandler(IEmailRepository repository)
        {
            _repository = repository;
        }

        private readonly IEmailRepository _repository;
        
        public async Task<ValidationResult> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var emails = await _repository.Get(e => e.UserId == request.UserId);

            if (emails.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um documento cadastrado."));

                if(emails.Any(e => e.Address == request.Address))
                    validation.Errors.Add(new ValidationFailure("", "O e-mail informado já está em uso."));

                return validation;
            }

            var email = new Email(request.Address, request.UserId);

            validation = email.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(email);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
