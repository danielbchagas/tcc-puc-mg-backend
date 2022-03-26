using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteBasketItemCommand : IRequest<ValidationResult>
    {
        public DeleteBasketItemCommand(Guid id, Guid basketId)
        {
            Id = id;
            BasketId = basketId;
        }

        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
    }
}
