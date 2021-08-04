﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }

    public class AtualizarClienteCommandValidation : AbstractValidator<AtualizarClienteCommand>
    {
        private string mensagemPropriedadeInvalida = "{PropertyName} é inválido!";
        private string mensagemPropriedadeExcedeuLimiteMaximo = "{PropertyName} excedeu o tamanho máximo!";

        public AtualizarClienteCommandValidation()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Nome)
                .MaximumLength(50)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(_ => _.Sobrenome)
                .MaximumLength(100)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(c => c.DataNascimento)
                .NotNull()
                .WithMessage(mensagemPropriedadeInvalida)
                .GreaterThan(DateTime.Now)
                .WithMessage(mensagemPropriedadeInvalida);
        }
    }
}
