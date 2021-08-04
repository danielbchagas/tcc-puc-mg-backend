using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarEnderecoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
        public bool EnderecoAtivo { get; set; }

        public Guid ClienteId { get; set; }
    }

    public class AtualizarEnderecoCommandValidation : AbstractValidator<AtualizarEnderecoCommand>
    {
        private readonly string mensagemPropriedadeInvalida = "{PropertyName} é inválido!";
        private readonly string mensagemPropriedadeExcedeuLimiteMaximo = "{PropertyName} excedeu o tamanho máximo!";

        public AtualizarEnderecoCommandValidation()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Logradouro)
                .MaximumLength(200)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Bairro)
                .MaximumLength(50)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Cidade)
                .MaximumLength(50)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Cep)
                .MaximumLength(9)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            // Estado não precisa ser validado
            RuleFor(_ => _.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
        }
    }
}
