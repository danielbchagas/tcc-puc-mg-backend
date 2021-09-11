using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.Cliente.Domain.Application.Handlers.Commands
{
    public class AdicionarDocumentoCommandHandler : IRequestHandler<AdicionarDocumentoCommand, ValidationResult>
    {
        public AdicionarDocumentoCommandHandler(IDocumentoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new DocumentoValidator();
        }

        private readonly IDocumentoRepository _repository;
        private readonly IMediator _mediator;
        private readonly DocumentoValidator _validador;

        public async Task<ValidationResult> Handle(AdicionarDocumentoCommand request, CancellationToken cancellationToken)
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

                if (sucesso)
                    await _mediator.Publish(new DocumentoCommitNotification(documentoId: documento.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(validation);
        }
    }
}
