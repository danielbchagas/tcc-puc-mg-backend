﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<(ValidationResult, Domain.Models.Customer)>
    {
        public CreateCustomerCommand(Guid id, string firstName, string lastName, CreateDocumentCommand document, CreateEmailCommand email, CreatePhoneCommand phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;

            Document = document;
            Email = email;
            Phone = phone;
        }

        public CreateCustomerCommand(Guid id, string firstName, string lastName, CreateDocumentCommand document, CreateEmailCommand email, CreatePhoneCommand phone, CreateAddressCommand address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;

            Document = document;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }

        public CreateDocumentCommand Document { get; set; }
        public CreateEmailCommand Email { get; set; }
        public CreatePhoneCommand Phone { get; set; }
        public CreateAddressCommand Address { get; set; }

        public ValidationResult Validate()
            => new CreateCustomerCommandValidator().Validate(this);
    }

    public class CreateDocumentCommand
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class CreateEmailCommand
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class CreatePhoneCommand
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class CreateAddressCommand 
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().NotNull();
            RuleFor(u => u.LastName).NotEmpty().NotNull();
            RuleFor(u => u.Document).NotNull();
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Phone).NotNull();
            RuleFor(a => a.Address).NotNull();
        }
    }
}
