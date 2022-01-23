using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class DisableClienteCommandHandler : IRequestHandler<DisableClienteCommand, ValidationResult>
    {
        public DisableClienteCommandHandler(IClienteRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IClienteRepository _repository;
        private readonly IMediator _mediator;

        public async Task<ValidationResult> Handle(DisableClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.Buscar(request.Id);

            if (cliente != null)
            {
                var valido = cliente.Validar();

                if (valido.IsValid)
                {
                    cliente.Ativo = false;

                    var sucesso = await _repository.UnitOfWork.Commit();
                }

                return await Task.FromResult(valido);
            }

            return await Task.FromResult(new ValidationResult());
        }
    }
}
