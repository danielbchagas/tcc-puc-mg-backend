using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteBasketItemCommand : IRequest<ValidationResult>
    {
        public DeleteBasketItemCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
