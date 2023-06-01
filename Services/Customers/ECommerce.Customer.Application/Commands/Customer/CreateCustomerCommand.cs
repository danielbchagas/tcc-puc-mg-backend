using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using Models = ECommerce.Customers.Domain.Models;

namespace ECommerce.Customers.Application.Commands.User
{
    public class CreateCustomerCommand : IRequest<ValidationResult>
    {
        public CreateCustomerCommand(Guid id, string firstName, string lastName, Models.Document document, Models.Email email, Models.Phone phone)
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

        public Models.Document Document { get; set; }
        public Models.Email Email { get; set; }
        public Models.Phone Phone { get; set; }
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
