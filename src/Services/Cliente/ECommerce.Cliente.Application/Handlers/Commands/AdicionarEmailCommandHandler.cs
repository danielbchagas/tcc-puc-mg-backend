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
    public class AdicionarEmailCommandHandler : IRequestHandler<AdicionarEmailCommand, ValidationResult>
    {
        public AdicionarEmailCommandHandler(IEmailRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IEmailRepository _repository;
        private readonly IMediator _mediator;
        
        public async Task<ValidationResult> Handle(AdicionarEmailCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var emailJaExiste = await _repository.Buscar(e => e.ClienteId == request.ClienteId);

            if (emailJaExiste.Count() > 0)
            {
                validation.Errors.Add(new ValidationFailure("", "O cliente já possui um documento cadastrado."));

                if(emailJaExiste.Any(e => e.Endereco == request.Endereco))
                    validation.Errors.Add(new ValidationFailure("", "O e-mail informado já está em uso."));

                return validation;
            }

            var email = new Email(request.Endereco, request.ClienteId);

            validation = email.Validar();

            if (validation.IsValid)
            {
                await _repository.Adicionar(email);
                var sucesso = await _repository.UnitOfWork.Commit();
            }

            return await Task.FromResult(validation);
        }
    }
}
