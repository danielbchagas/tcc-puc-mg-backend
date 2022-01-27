using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteBasketItemCommand : IRequest<ValidationResult>
    {
        public DeleteBasketItemCommand(Guid id, Guid userId)
        {
            Id = id;

            UserId = userId;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
