using ECommerce.Customers.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Customers.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        public User(Guid id, string firstName, string lastName, bool enabled = true)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Enabled = enabled;
        }

        public User(Guid id, string firstName, string lastName, Document document, Email email, Phone phone, bool enabled = true)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Enabled = enabled;

            Document = document;
            Email = email;
            Phone = phone;
        }

        public User(Guid id, string firstName, string lastName, Document document, Email email, Phone phone, Address address, bool enabled = true)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Enabled = enabled;

            Document = document;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }

        public Document Document { get; set; }
        public Email Email { get; set; }
        public Phone Phone { get; set; }
        public Address Address { get; set; }

        public ValidationResult Validate()
        {
            return new CustomerValidator().Validate(this);
        }
    }

    public class CustomerValidator : AbstractValidator<User>
    {
        public CustomerValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.FirstName)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.LastName)
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
