using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class CreatePhoneCommand : IRequest<ValidationResult>
    {
        public CreatePhoneCommand(Guid id, string number, Guid customerId)
        {
            Id = id;
            Number = number;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }
}
