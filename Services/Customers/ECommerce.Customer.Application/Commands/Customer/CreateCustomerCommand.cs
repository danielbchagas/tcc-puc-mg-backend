using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Commands.User
{
    public class CreateCustomerCommand : IRequest<ValidationResult>
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

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CreateDocumentCommand Document { get; set; }
        public CreateEmailCommand Email { get; set; }
        public CreatePhoneCommand Phone { get; set; }
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

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().NotNull();
            RuleFor(u => u.LastName).NotEmpty().NotNull();
            RuleFor(u => u.Document).NotNull();
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Phone).NotNull();
        }
    }
}
