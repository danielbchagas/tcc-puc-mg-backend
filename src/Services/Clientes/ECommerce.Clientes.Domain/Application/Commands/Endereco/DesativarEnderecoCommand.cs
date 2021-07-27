using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands.Endereco
{
    public class DesativarEnderecoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
    }

    public class DesativarEnderecoCommandValidation : AbstractValidator<DesativarEnderecoCommand>
    {
        public DesativarEnderecoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.ClienteId).NotEqual(Guid.Empty).WithMessage("{PropertyName} inválido!");
        }
    }
}
