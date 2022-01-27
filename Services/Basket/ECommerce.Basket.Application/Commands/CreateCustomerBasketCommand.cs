using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class CreateCustomerBasketCommand : IRequest<ValidationResult>
    {
        public CreateCustomerBasketCommand(Guid id, decimal value, Guid customerId)
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
