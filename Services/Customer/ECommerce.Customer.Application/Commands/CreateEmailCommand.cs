using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class CreateEmailCommand : IRequest<ValidationResult>
    {
        public CreateEmailCommand(Guid id, string address, Guid customerId)
        {
            Id = id;
            Address = address;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid CustomerId { get; set; }
    }
}
