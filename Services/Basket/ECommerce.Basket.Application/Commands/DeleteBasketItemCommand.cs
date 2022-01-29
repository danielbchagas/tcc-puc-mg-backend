using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteBasketItemCommand : IRequest<ValidationResult>
    {
        public DeleteBasketItemCommand(Guid id, int quantity, Guid customerBasketId)
        {
            Id = id;
            Quantity = quantity;
            CustomerBasketId = customerBasketId;
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid CustomerBasketId { get; set; }
    }
}
