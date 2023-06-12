using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Baskets.Application.Commands.Item
{
    public class IncludeItemCommand : IRequest<(ValidationResult, Domain.Models.Basket)>
    {
        public Guid BasketId { get; set; }

        public IEnumerable<ItemCommand> Items { get; set; }
    }

    public class ItemCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
