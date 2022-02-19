using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Customer.Application.Commands
{
    public class UpdateAddressCommand : IRequest<ValidationResult>
    {
        public UpdateAddressCommand(Guid id, string firstLine, string secondLine, string city, string zipCode, string state, Guid userId)
        {
            Id = id;
            FirstLine = firstLine;
            SecondLine = secondLine;
            City = city;
            ZipCode = zipCode;
            State = state;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public Guid UserId { get; set; }
    }
}
