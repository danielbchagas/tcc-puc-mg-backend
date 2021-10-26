﻿using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Email : Entity
    {
        #region Construtores
        public Email(string endereco)
        {
            Endereco = endereco;
        }
        #endregion

        #region Propriedades
        public string Endereco { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new EmailValidator().Validate(this);
        }
        #endregion
    }

    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Endereco)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .EmailAddress();
        }
    }
}