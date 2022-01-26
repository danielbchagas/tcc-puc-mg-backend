using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands
{
    public class UpdateEmailCommand : IRequest<ValidationResult>
    {
        public UpdateEmailCommand(Guid id, string address, Guid customerId)
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
