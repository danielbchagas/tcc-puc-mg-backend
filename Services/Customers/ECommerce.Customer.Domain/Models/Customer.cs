using ECommerce.Customers.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Customers.Domain.Models
{
    public class Customer : Entity, IAggregateRoot, IAuditable
    {
        public Customer(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTime.Now;
        }

        public Customer(Guid id, string firstName, string lastName, DateTime createdAt, Document document, Email email, Phone phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;

            Document = document;
            Email = email;
            Phone = phone;
        }

        public Customer(Guid id, string firstName, string lastName, DateTime createdAt, Document document, Email email, Phone phone, Address address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;

            Document = document;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Document Document { get; set; }
        public Email Email { get; set; }
        public Phone Phone { get; set; }
        public Address Address { get; set; }
        
        public ValidationResult Validate()
        {
            return new CustomerValidator().Validate(this);
        }
    }

    public class CustomerValidator : AbstractValidator<Customer>
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
