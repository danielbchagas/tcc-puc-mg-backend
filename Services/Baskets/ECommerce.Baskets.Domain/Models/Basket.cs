using ECommerce.Basket.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Basket.Domain.Models
{
    public class Basket : IAggregateRoot, IAuditable
    {
        public Basket(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
            IsEnded = false;
            Items = new List<Item>();
        }

        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsEnded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Item> Items { get; set; }

        public ValidationResult UpdatesItems(Item item)
        {
            var validationResult = item.Validate();

            if (!validationResult.IsValid)
                return validationResult;

            var exists = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (exists != null)
            {
                exists.Quantity = item.Quantity;
                Items.Remove(exists);
                Items.Add(exists);
            }
            else
            {
                Items.Add(item);
            }

            return Validate();
        }

        public ValidationResult RemoveItem(Item item)
        {
            var validationResult = item.Validate();

            if (!validationResult.IsValid)
                return validationResult;

            var exists = Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (exists != null)
            {
                Items.Remove(exists);
            }

            return Validate();
        }

        public ValidationResult Validate()
        {
            return new ShoppingBasketValidator().Validate(this);
        }

        public ValidationResult UpdateBasketValue()
        {
            Value = Items.Sum(i => i.Quantity * i.Value);

            return Validate();
        }
    }

    public class ShoppingBasketValidator : AbstractValidator<Basket>
    {
        public ShoppingBasketValidator()
        {
            RuleFor(ci => ci.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
