using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AtualizarEmailCommandHandler : IRequestHandler<AtualizarEmailCommand, ValidationResult>
    {
        public AtualizarEmailCommandHandler(IEmailRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IEmailRepository _repository;
        private readonly IMediator _mediator;
        
        public async Task<ValidationResult> Handle(AtualizarEmailCommand request, CancellationToken cancellationToken)
        {
            var email = await _repository.Buscar(request.Id);
            email.Endereco = request.Endereco;

            var valido = email.Validar();

            if (valido.IsValid)
            {
                await _repository.Adicionar(email);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EmailCommitNotification(emailId: email.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }
    }
}
