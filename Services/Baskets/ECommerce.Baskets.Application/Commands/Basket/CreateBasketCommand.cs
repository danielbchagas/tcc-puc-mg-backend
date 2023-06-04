using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Baskets.Application.Commands.Basket
{
    public class CreateBasketCommand : IRequest<(ValidationResult, Domain.Models.Basket)>
    {
        public CreateBasketCommand(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
