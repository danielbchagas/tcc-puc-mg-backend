using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Carts.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Carts.Domain.Models
{
    public class Cart : Entity, IAggregateRoot
    {
        public Cart(Guid customerId)
        {
            CustomerId = customerId;
            Itens = new List<Item>();
        }

        public decimal Value { get; set; }
        public Guid CustomerId { get; set; }

        public ICollection<Item> Itens { get; set; }

        public ValidationResult AddItens(Item item)
        {
            var validationResult = item.Validate();

            if (!validationResult.IsValid)
                return validationResult;

            Itens.Add(item);

            Value += Itens.Sum(i => i.Quantity * i.Value);

            return Validate();
        }

        public ValidationResult Validate()
        {
            return new CartValidator().Validate(this);
        }
    }

    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(ci => ci.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
