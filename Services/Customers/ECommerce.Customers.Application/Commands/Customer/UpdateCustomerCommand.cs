using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands.Customer
{
    public class UpdateCustomerCommand : IRequest<(ValidationResult, Domain.Models.Customer)>
    {
        public UpdateCustomerCommand(Guid id, string firstName, string lastName, bool enabled)
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

        public UpdatePhoneCommand Phone { get; set; }
        public UpdateDocumentCommand Document { get; set; }
        public UpdateAddressCommand Address { get; set; }
    }

    public class UpdatePhoneCommand
    {
        public UpdatePhoneCommand(Guid id, string number, Guid customerId)
        {
            Id = id;
            Number = number;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class UpdateDocumentCommand
    {
        public UpdateDocumentCommand(Guid id, string number, Guid customerId)
        {
            Id = id;
            Number = number;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class UpdateAddressCommand
    {
        public UpdateAddressCommand(Guid id, string firstLine, string secondLine, string city, string zipCode, string state, Guid customerId)
        {
            Id = id;
            FirstLine = firstLine;
            SecondLine = secondLine;
            City = city;
            ZipCode = zipCode;
            State = state;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; set; }
    }
}
