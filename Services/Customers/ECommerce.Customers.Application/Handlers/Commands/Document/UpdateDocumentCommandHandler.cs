using ECommerce.Customers.Application.Commands.Document;
using ECommerce.Customers.Domain.Interfaces.Data;
using ECommerce.Customers.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Handlers.Commands.Document
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, ValidationResult>
    {
        public UpdateDocumentCommandHandler(IDocumentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private readonly IDocumentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<ValidationResult> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _repository.Get(request.Id);
            document.Number = request.Number;

            var validation = document.Validate();

            if (validation.IsValid)
            {
                await _repository.Update(document);
                await _unitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
