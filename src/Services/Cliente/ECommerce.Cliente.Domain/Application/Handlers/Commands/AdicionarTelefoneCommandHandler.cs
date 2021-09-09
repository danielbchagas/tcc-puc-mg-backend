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
    public class AdicionarTelefoneCommandHandler : IRequestHandler<AdicionarTelefoneCommand, ValidationResult>
    {
        public AdicionarTelefoneCommandHandler(ITelefoneRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly ITelefoneRepository _repository;
        private readonly IMediator _mediator;
        
        public async Task<ValidationResult> Handle(AdicionarTelefoneCommand request, CancellationToken cancellationToken)
        {
            var telefone = new Telefone(request.Numero, request.ClienteId);

            var valido = telefone.Validar();

            if (valido.IsValid)
            {
                await _repository.Adicionar(telefone);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new TelefoneCommitNotification(telefoneId: telefone.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }
    }
}
