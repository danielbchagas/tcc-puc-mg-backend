using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Commands.User
{
    public class CreateUserCommand : IRequest<ValidationResult>
    {
        public CreateUserCommand(Guid id, string firstName, string lastName, bool enabled, Domain.Models.Document document, Domain.Models.Email email, Domain.Models.Phone phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Enabled = enabled;

            Document = document;
            Email = email;
            Phone = phone;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }

        public Domain.Models.Document Document { get; set; }
        public Domain.Models.Email Email { get; set; }
        public Domain.Models.Phone Phone { get; set; }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().NotNull();
            RuleFor(u => u.LastName).NotEmpty().NotNull();
            RuleFor(u => u.Document).NotNull();
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Phone).NotNull();
        }
    }
}
