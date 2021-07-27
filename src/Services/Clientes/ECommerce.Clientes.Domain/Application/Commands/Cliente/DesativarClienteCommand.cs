using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands.Cliente
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
    }

    public class DesativarClienteCommandValidation : AbstractValidator<DesativarClienteCommand>
    {
        public DesativarClienteCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} inválido!");
        }
    }
}
