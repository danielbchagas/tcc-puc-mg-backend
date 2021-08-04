using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AdicionarEmailCommand : IRequest<ValidationResult>
    {
        public AdicionarEmailCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }

    public class AdicionarEmailCommandValidator : AbstractValidator<AdicionarEmailCommand>
    {
        private readonly string mensagemPropriedadeInvalida = "{PropertyName} é inválido!";
        private readonly string mensagemPropriedadeExcedeuLimiteMaximo = "{PropertyName} excedeu o tamanho máximo!";

        public AdicionarEmailCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(d => d.Endereco)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida)
                .MaximumLength(100)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo);
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
        }
    }
}
