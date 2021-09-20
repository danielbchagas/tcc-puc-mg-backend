using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Application.Notifications;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class AdicionarEnderecoCommandHandler : IRequestHandler<AdicionarEnderecoCommand, ValidationResult>
    {
        public AdicionarEnderecoCommandHandler(IEnderecoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
            _validador = new EnderecoValidator();
        }

        private readonly IEnderecoRepository _repository;
        private readonly EnderecoValidator _validador;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(AdicionarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var enderecoJaExiste = await _repository.Buscar(d => d.ClienteId == request.ClienteId);

            if (enderecoJaExiste.Count() > 0)
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um endereço cadastrado."));

                return validation;
            }

            var endereco = new Endereco(request.Logradouro, request.Bairro, request.Cidade, request.Cep, request.Estado, request.ClienteId);

            validation = _validador.Validate(endereco);

            if (validation.IsValid)
            {
                await _repository.Adicionar(endereco);
                var sucesso = await _repository.UnitOfWork.Commit();

                if (sucesso)
                    await _mediator.Publish(new EnderecoCommitNotification(enderecoId: endereco.Id, request.ClienteId));
            }

            return await Task.FromResult(validation);
        }
    }
}
