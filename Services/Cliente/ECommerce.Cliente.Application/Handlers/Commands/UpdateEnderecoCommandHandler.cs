using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class UpdateEnderecoCommandHandler : IRequestHandler<UpdateEnderecoCommand, ValidationResult>
    {
        public UpdateEnderecoCommandHandler(IEnderecoRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IEnderecoRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(UpdateEnderecoCommand request, CancellationToken cancellationToken)
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
            }

            return await Task.FromResult(valido);
        }
    }
}
