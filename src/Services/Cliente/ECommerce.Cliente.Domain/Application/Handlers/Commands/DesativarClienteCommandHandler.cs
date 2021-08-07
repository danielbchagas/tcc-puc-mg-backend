using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Handlers.Commands
{
    public class DesativarClienteCommandHandler : IRequestHandler<DesativarClienteCommand, ValidationResult>
    {
        public DesativarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validador = new ClienteValidator();
            _mediator = mediator;
        }

        private readonly IClienteRepository _repository;
        private readonly ClienteValidator _validador;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(DesativarClienteCommand request, CancellationToken cancellationToken)
        {
            var valido = new ValidationResult();

            var cliente = await _repository.Buscar(request.Id);

            if (cliente != null)
            {
                valido = _validador.Validate(cliente);

                if (valido.IsValid)
                {
                    cliente.Desativar();

                    var sucesso = await _repository.UnitOfWork.Commit();

                    if (sucesso)
                        await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: Guid.NewGuid()));
                }
            }

            return await Task.FromResult(valido);
        }
    }
}
