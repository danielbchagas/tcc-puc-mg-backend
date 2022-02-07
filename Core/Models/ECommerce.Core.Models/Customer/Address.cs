using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Core.Models.Customer
{
    public class Address : Entity
    {
        public Address(string firstLine, string secondLine, string city, string zipCode, State state, Guid customerId)
        {
            FirstLine = firstLine;
            SecondLine = secondLine;
            City = city;
            State = state;
            ZipCode = zipCode;
            CustomerId = customerId;
        }

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public User Customer { get; set; }

        public ValidationResult Validate()
        {
            return new AddressValidator().Validate(this);
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.FirstLine)
                .MaximumLength(200)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.SecondLine)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.City)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.ZipCode)
                .MaximumLength(9)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}