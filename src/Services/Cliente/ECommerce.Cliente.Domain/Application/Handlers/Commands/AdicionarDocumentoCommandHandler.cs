using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            var documento = new Documento(request.Numero, request.ClienteId);

            var valido = _validador.Validate(documento);

            if (valido.IsValid)
            {
                await _repository.Adicionar(documento);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new DocumentoCommitNotification(documentoId: documento.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }
    }
}
