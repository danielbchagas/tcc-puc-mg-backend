using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AtualizarDocumentoCommandHandler : IRequestHandler<AtualizarDocumentoCommand, ValidationResult>
    {
        public AtualizarDocumentoCommandHandler(IDocumentoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IDocumentoRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarDocumentoCommand request, CancellationToken cancellationToken)
        {
            var documento = await _repository.Buscar(request.Id);
            documento.Numero = request.Numero;

            var valido = documento.Validar();

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
