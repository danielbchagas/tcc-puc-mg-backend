﻿using FluentValidation;
using System;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace ECommerce.Core.Models.Customer
{
    public class Document : Entity
    {
        public Document(string number, Guid customerId)
        {
            Number = number;
            CustomerId = customerId;
        }

        public string Number { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public User Customer { get; set; }

        public ValidationResult Validate()
        {
            return new DocumentValidator().Validate(this);
        }
    }

    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(18)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
            RuleFor(d => d.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
