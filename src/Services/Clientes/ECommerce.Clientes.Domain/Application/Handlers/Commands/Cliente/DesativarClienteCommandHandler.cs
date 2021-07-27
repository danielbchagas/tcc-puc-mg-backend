using ECommerce.Clientes.Domain.Application.Commands.Cliente;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands.Cliente
{
    public class DesativarClienteCommandHandler : IRequestHandler<DesativarClienteCommand, ValidationResult>
    {
        public DesativarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _validacoes = new DesativarClienteCommandValidation();
            _mediator = mediator;
        }

        private readonly IClienteRepository _repository;
        private readonly DesativarClienteCommandValidation _validacoes;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(DesativarClienteCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var cliente = await _repository.Buscar(request.Id);

                if(cliente != null)
                {
                    cliente.Desativar();

                    var sucesso = await _repository.UnitOfWork.Commit();

                    if (sucesso)
                        await _mediator.Publish(new ClienteCommitNotification("", "", cliente.Id));
                }
            }

            return await Task.FromResult(valido);
        }
    }
}
