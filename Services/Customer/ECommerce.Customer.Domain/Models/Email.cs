using System;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Customer.Domain.Models
{
    public class Email : Entity
    {
        public Email(string address, Guid userId)
        {
            Address = address;
            UserId = userId;
        }

        public string Address { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ValidationResult Validate()
        {
            return new EmailValidator().Validate(this);
        }
    }

    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .EmailAddress();
            RuleFor(d => d.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
