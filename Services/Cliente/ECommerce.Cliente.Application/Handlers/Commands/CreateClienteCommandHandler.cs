using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ValidationResult>
    {
        public CreateClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IClienteRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Domain.Models.Cliente(id: request.Id, nome: request.Nome, sobrenome: request.Sobrenome, ativo: request.Ativo, documento: request.Documento, email: request.Email, telefone: request.Telefone);

            var validacao = cliente.Validar();

            if (validacao.IsValid)
            {
                await _repository.Adicionar(cliente);
                var sucesso = await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validacao);
        }
    }
}
