using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ValidationResult>
    {
        public AtualizarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IClienteRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.Buscar(request.Id);
            cliente.Nome = request.Nome;
            cliente.Sobrenome = request.Sobrenome;
            cliente.Ativo = request.Ativo;

            var validacao = cliente.Validar();

            if (validacao.IsValid)
            {
                await _repository.Atualizar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: request.Id));
            }

            return await Task.FromResult(validacao);
        }
    }
}
