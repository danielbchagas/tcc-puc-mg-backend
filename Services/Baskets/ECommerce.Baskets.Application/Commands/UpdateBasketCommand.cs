using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Basket.Application.Commands
{
    public class UpdateBasketCommand : IRequest<(ValidationResult, Domain.Models.Basket)>
    {
        public UpdateBasketCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public IEnumerable<UpdateItemCommand> Items { get; set; }
    }

    public class UpdateItemCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
    }
}
