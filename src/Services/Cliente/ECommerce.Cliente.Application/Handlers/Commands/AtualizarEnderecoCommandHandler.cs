using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AtualizarEnderecoCommandHandler : IRequestHandler<AtualizarEnderecoCommand, ValidationResult>
    {
        public AtualizarEnderecoCommandHandler(IEnderecoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IEnderecoRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AtualizarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var endereco = await _repository.Buscar(request.Id);
            endereco.Logradouro = request.Logradouro;
            endereco.Bairro = request.Bairro;
            endereco.Cidade = request.Cidade;
            endereco.Cep = request.Cep;
            endereco.Estado = request.Estado;

            var valido = endereco.Validar();

            if (valido.IsValid)
            {
                await _repository.Atualizar(endereco);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EnderecoCommitNotification(enderecoId: endereco.Id, usuarioId: request.ClienteId));
            }

            return await Task.FromResult(valido);
        }
    }
}
