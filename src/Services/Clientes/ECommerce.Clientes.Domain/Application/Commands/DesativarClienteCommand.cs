using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
    }

    public class DesativarClienteCommandValidation : AbstractValidator<DesativarClienteCommand>
    {
        public DesativarClienteCommandValidation()
        {
            var mensagemPropriedadeInvalida = "{PropertyName} é inválido!";

            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
        }
    }
}
