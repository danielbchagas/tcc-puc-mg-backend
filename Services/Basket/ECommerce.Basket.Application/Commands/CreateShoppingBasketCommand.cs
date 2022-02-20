using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class CreateShoppingBasketCommand : IRequest<ValidationResult>
    {
        public CreateShoppingBasketCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
