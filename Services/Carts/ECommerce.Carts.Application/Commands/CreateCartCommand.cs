using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Commands
{
    public class CreateCartCommand : IRequest<ValidationResult>
    {
        public CreateCartCommand(Guid id, decimal value, Guid customerId)
        {
            Id = id;
            Value = value;

            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public decimal Value { get; set; }

        public Guid CustomerId { get; set; }
    }
}
