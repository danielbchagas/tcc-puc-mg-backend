﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AdicionarDocumentoCommand : IRequest<ValidationResult>
    {
        public AdicionarDocumentoCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }

    public class CadastrarDocumentoCommandValidation : AbstractValidator<AdicionarDocumentoCommand>
    {
        private readonly string mensagemPropriedadeInvalida = "{PropertyName} é inválido!";
        private readonly string mensagemPropriedadeExcedeuLimiteMaximo = "{PropertyName} excedeu o tamanho máximo!";

        public CadastrarDocumentoCommandValidation()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
            RuleFor(d => d.Numero)
                .NotNull()
                .NotEmpty()
                .WithMessage(mensagemPropriedadeInvalida)
                .MaximumLength(18)
                .WithMessage(mensagemPropriedadeExcedeuLimiteMaximo);
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(mensagemPropriedadeInvalida);
        }
    }
}
