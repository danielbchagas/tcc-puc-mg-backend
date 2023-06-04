using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Baskets.Application.Commands.Item
{
    public class RemoveItemCommand : IRequest<(ValidationResult, Domain.Models.Basket)>
    {
        public RemoveItemCommand(Guid id)
        {
            BasketId = id;
        }

        public Guid BasketId { get; set; }

        public ItemCommand Item { get; set; }
    }
}
