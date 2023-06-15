using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Customers.Application.Commands.Address
{
    public class CreateAddressCommand : IRequest<(ValidationResult, Domain.Models.Address)>
    {
        public CreateAddressCommand(Guid id, string firstLine, string secondLine, string city, string zipCode, string state, Guid customerId)
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
