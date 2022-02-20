using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteShoppingBasketCommand : IRequest<ValidationResult>
    {
        public DeleteShoppingBasketCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
