using System;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Customers.Domain.Models
{
    public class Phone : Entity
    {
        public Phone(string number, Guid userId)
        {
            Number = number;
            UserId = userId;
        }

        public string Number { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ValidationResult Validate()
        {
            return new PhoneValidator().Validate(this);
        }
    }

    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
            RuleFor(d => d.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
