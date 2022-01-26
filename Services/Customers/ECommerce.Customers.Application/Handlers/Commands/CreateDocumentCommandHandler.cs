using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using ECommerce.Customers.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Commands
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

            var documents = await _repository.Get(d => d.CustomerId == request.CustomerId);

            if(documents.Any())
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um documento cadastrado."));

                if(documents.Any(d => d.Number == request.Number))
                    validation.Errors.Add(new ValidationFailure("", "O documento informado já está em uso."));

                return validation;
            }

            var document = new Document(request.Number, request.CustomerId);

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
