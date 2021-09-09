using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
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
        }

        private readonly IClienteRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Models.Cliente(id: request.Id, nome: request.Nome, sobrenome: request.Sobrenome, request.Ativo);

            var validacao = cliente.Validar();

            if (validacao.IsValid)
            {
                await _repository.Adicionar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();

                #region Composições
                // Documento
                try
                {
                    var documentoValido = await _mediator.Send(new AdicionarDocumentoCommand(request.Documento, request.Id));

                    if (!documentoValido.IsValid)
                    {
                        await _repository.Excluir(cliente.Id);
                        await _repository.UnitOfWork.Commit();

                        return await Task.FromResult(documentoValido);
                    }

                    // Telefone
                    var telefoneValido = await _mediator.Send(new AdicionarTelefoneCommand(request.Telefone, request.Id));

                    if (!telefoneValido.IsValid)
                    {
                        await _repository.Excluir(cliente.Id);
                        await _repository.UnitOfWork.Commit();

                        return await Task.FromResult(telefoneValido);
                    }

                    // Email
                    var emailValido = await _mediator.Send(new AdicionarEmailCommand(request.Email, request.Id));

                    if (!emailValido.IsValid)
                    {
                        await _repository.Excluir(cliente.Id);
                        await _repository.UnitOfWork.Commit();

                        return await Task.FromResult(emailValido);
                    }
                }
                catch (Exception)
                {
                    if (sucesso)
                    {
                        await _repository.Excluir(cliente.Id);
                        await _repository.UnitOfWork.Commit();
                    }

                    throw;
                }
                #endregion

                if (sucesso)
                    await _mediator.Publish(new ClienteCommitNotification(clienteId: cliente.Id, usuarioId: request.Id));
            }

            return await Task.FromResult(validacao);
        }
    }
}
