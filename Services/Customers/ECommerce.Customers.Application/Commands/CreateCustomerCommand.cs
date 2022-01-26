using System;
using ECommerce.Customers.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands
{
    public class CreateCustomerCommand : IRequest<ValidationResult>
    {
        public CreateCustomerCommand(Guid id, string firstName, string lastName, bool enabled, Document document, Email email, Phone phone)
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

        public Document Document { get; set; }
        public Email Email { get; set; }
        public Phone Phone { get; set; }
    }
}
