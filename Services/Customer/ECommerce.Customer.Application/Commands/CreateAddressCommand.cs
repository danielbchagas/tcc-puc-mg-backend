using System;
using ECommerce.Customer.Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class CreateAddressCommand : IRequest<ValidationResult>
    {
        public CreateAddressCommand(Guid id, string firstLine, string secondLine, string city, string zipCode, State state, Guid userId)
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
        public State State { get; set; }
        public Guid UserId { get; set; }
    }
}
