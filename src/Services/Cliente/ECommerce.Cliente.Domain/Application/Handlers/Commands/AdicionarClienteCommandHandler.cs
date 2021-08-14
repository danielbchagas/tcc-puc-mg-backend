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
    public class AdicionarClienteCommandHandler : IRequestHandler<AdicionarClienteCommand, ValidationResult>
    {
        public AdicionarClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _clienteValidador = new ClienteValidator();
        }

        private readonly IClienteRepository _repository;
        private readonly ClienteValidator _clienteValidador;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Models.Cliente(id: request.Id, nome: request.Nome, sobrenome: request.Sobrenome);

            var clienteValido = _clienteValidador.Validate(cliente);

            if (clienteValido.IsValid)
            {
                await _repository.Adicionar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                #region Agregações
                // Documento
                var documentoValido = await _mediator.Send(new AdicionarDocumentoCommand(request.Documento, request.Id));
                
                if (!documentoValido.IsValid)
                {
                    await _repository.Excluir(cliente.Id);
                    return await Task.FromResult(documentoValido);
                }

                // Telefone
                var telefoneValido = await _mediator.Send(new AdicionarTelefoneCommand(request.Telefone, request.Id));
                
                if (!telefoneValido.IsValid)
                    return await Task.FromResult(telefoneValido);
                
                // Email
                var emailValido = await _mediator.Send(new AdicionarEmailCommand(request.Email, request.Id));

                if (!emailValido.IsValid)
                    return await Task.FromResult(emailValido);
                #endregion

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: request.Id));
            }

            return await Task.FromResult(clienteValido);
        }
    }
}
