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
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, ValidationResult>
    {
        public CreateDocumentCommandHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        private readonly IDocumentRepository _repository;

        public async Task<ValidationResult> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var documents = await _repository.Get(d => d.UserId == request.UserId);

            if(documents.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um documento cadastrado."));

                if(documents.Any(d => d.Number == request.Number))
                    validation.Errors.Add(new ValidationFailure("", "O documento informado já está em uso."));

                return validation;
            }

            var document = new Document(request.Number, request.UserId);

            validation = document.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(document);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
