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
    public class AdicionarEmailCommandHandler : IRequestHandler<AdicionarEmailCommand, ValidationResult>
    {
        public AdicionarEmailCommandHandler(IEmailRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IEmailRepository _repository;
        private readonly IMediator _mediator;
        
        public async Task<ValidationResult> Handle(AdicionarEmailCommand request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Endereco, request.ClienteId);

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
