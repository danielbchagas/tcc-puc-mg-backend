using ECommerce.Baskets.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Baskets.Domain.Models
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
        public bool IsEnded { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid CustomerId { get; set; }

        public ICollection<Item> Items { get; set; }

        public ValidationResult AddOrUpdateItem(Item item)
        {
            var validation = item.Validate();

            if (!validation.IsValid)
                return validation;

            var index = Items.ToList().FindIndex(f => f.Id == item.Id && f.DeletedAt == null);

            if (index != -1)
            {
                Items.ElementAt(index).UpdatedAt = DateTime.Now;
                Items.ElementAt(index).Quantity = item.Quantity;
            }
            else
            {
                item.LinkToBasket(Id);
                Items.Add(item);
            }

            UpdateBasketValue();

            return validation;
        }

        public ValidationResult RemoveItems(Item item)
        {
            var validation = item.Validate();

            if (!validation.IsValid)
                return validation;

            var index = Items.ToList().FindIndex(i => i.Id == item.Id);

            if (index != -1)
            {
                Items.ElementAt(index).DeletedAt = DateTime.Now;
            }

            UpdateBasketValue();

            return validation;
        }

        public void UpdateBasketValue()
        {
            Value = Items.Sum(i => i.Quantity * i.Value);
        }

        public void EndBasket() => IsEnded = true;

        public ValidationResult Validate() => new BasketValidator().Validate(this);
    }

    public class BasketValidator : AbstractValidator<Basket>
    {
        public BasketValidator()
        {
            RuleFor(ci => ci.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
