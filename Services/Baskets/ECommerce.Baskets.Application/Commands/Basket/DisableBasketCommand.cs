using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Baskets.Application.Commands.Basket
{
    public class DisableBasketCommand : IRequest<ValidationResult>
    {
        public DisableBasketCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
