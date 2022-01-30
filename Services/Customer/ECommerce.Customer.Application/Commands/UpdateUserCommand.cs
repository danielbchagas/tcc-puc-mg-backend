using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class UpdateUserCommand : IRequest<ValidationResult>
    {
        public UpdateUserCommand(Guid id, string firstName, string lastName, bool enabled)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Enabled = enabled;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
    }
}
