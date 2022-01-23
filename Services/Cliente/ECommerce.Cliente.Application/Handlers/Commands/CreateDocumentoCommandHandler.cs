using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class CreateDocumentoCommandHandler : IRequestHandler<CreateDocumentoCommand, ValidationResult>
    {
        public CreateDocumentoCommandHandler(IDocumentoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new DocumentoValidator();
        }

        private readonly IDocumentoRepository _repository;
        private readonly IMediator _mediator;
        private readonly DocumentoValidator _validador;

        public async Task<ValidationResult> Handle(CreateDocumentoCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var documentoJaExiste = await _repository.Buscar(d => d.ClienteId == request.ClienteId);

            if(documentoJaExiste.Count() > 0)
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um documento cadastrado."));

                if(documentoJaExiste.Any(d => d.Numero == request.Numero))
                    validation.Errors.Add(new ValidationFailure("", "O documento informado já está em uso."));

                return validation;
            }

            var documento = new Documento(request.Numero, request.ClienteId);

            validation = _validador.Validate(documento);

            if (validation.IsValid)
            {
                await _repository.Adicionar(documento);
                var sucesso = await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
