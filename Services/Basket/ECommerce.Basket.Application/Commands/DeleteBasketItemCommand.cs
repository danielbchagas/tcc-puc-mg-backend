using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteBasketItemCommand : IRequest<ValidationResult>
    {
        public DeleteBasketItemCommand(Guid id, Guid customerBasketId)
        {
            Id = id;
            CustomerBasketId = customerBasketId;
        }

        public Guid Id { get; set; }
        public Guid CustomerBasketId { get; set; }
    }
}
