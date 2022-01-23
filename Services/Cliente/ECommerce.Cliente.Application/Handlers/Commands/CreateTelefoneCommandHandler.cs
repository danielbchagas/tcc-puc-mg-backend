using ECommerce.Cliente.Application.Commands;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Commands
{
    public class CreateTelefoneCommandHandler : IRequestHandler<CreateTelefoneCommand, ValidationResult>
    {
        public CreateTelefoneCommandHandler(ITelefoneRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly ITelefoneRepository _repository;
        private readonly IMediator _mediator;
        
        public async Task<ValidationResult> Handle(CreateTelefoneCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var telefoneJaExiste = await _repository.Buscar(t => t.ClienteId == request.ClienteId);

            if(telefoneJaExiste.Count() > 0)
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um telefone cadastrado."));

                if (telefoneJaExiste.Any(t => t.Numero == request.Numero))
                    validation.Errors.Add(new ValidationFailure("", "O telefone informado já está em uso."));

                return validation;
            }

            var telefone = new Telefone(request.Numero, request.ClienteId);

            validation = telefone.Validar();

            if (validation.IsValid)
            {
                await _repository.Adicionar(telefone);
                var sucesso = await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
