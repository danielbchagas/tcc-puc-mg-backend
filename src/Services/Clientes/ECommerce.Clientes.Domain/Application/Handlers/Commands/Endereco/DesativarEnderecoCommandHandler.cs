using ECommerce.Clientes.Domain.Application.Commands.Endereco;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands.Endereco
{
    public class DesativarEnderecoCommandHandler : IRequestHandler<DesativarEnderecoCommand, ValidationResult>
    {
        public DesativarEnderecoCommandHandler(IEnderecoRepository repository, IClienteRepository clienteRepository, IMediator mediator)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _validacoes = new DesativarEnderecoCommandValidation();
            _mediator = mediator;
        }

        private readonly IEnderecoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly DesativarEnderecoCommandValidation _validacoes;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(DesativarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var valido = _validacoes.Validate(request);

            if (valido.IsValid)
            {
                var endereco = await _repository.Buscar(request.Id);

                if (endereco != null)
                {
                    endereco.Desativar();

                    var sucesso = await _repository.UnitOfWork.Commit();

                    if (sucesso)
                        await _mediator.Publish(new EnderecoCommitNotification("", "", endereco.Id));
                }
            }

            return await Task.FromResult(valido);
        }
    }
}
