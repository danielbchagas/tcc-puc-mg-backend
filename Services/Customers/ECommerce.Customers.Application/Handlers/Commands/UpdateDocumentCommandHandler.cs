using System.Threading;
using System.Threading.Tasks;
using ECommerce.Customers.Application.Commands;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Handlers.Commands
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, ValidationResult>
    {
        public UpdateDocumentCommandHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        private readonly IDocumentRepository _repository;
        
        public async Task<ValidationResult> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _repository.Get(request.Id);
            document.Number = request.Number;

            var validation = document.Validate();

            if (validation.IsValid)
            {
                await _repository.Create(document);
                await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
