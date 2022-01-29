using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Basket.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Basket.Domain.Models
{
    public class CustomerBasket : Entity, IAggregateRoot
    {
        public CustomerBasket(Guid customerId)
        {
            CustomerId = customerId;
            Items = new List<BasketItem>();
        }

        public decimal Value { get; set; }
        public Guid CustomerId { get; set; }

        public ICollection<BasketItem> Items { get; set; }

        public ValidationResult UpdatesItems(BasketItem item)
        {
            var validationResult = item.Validate();

            if (!validationResult.IsValid)
                return validationResult;

            var exists = Items.FirstOrDefault(i => i.Id == item.Id);

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

            Value = Items.Sum(i => i.Quantity * i.Value);

            return Validate();
        }

        public ValidationResult RemoveItem(BasketItem item)
        {
            var validationResult = item.Validate();

            if (!validationResult.IsValid)
                return validationResult;

            var exists = Items.FirstOrDefault(i => i.Id == item.Id);

            if (exists != null)
            {
                Items.Remove(exists);
            }

            Value = Items.Sum(i => i.Quantity * i.Value);

            return Validate();
        }

        public ValidationResult Validate()
        {
            return new CustomerBasketValidator().Validate(this);
        }
    }

    public class CustomerBasketValidator : AbstractValidator<CustomerBasket>
    {
        public CustomerBasketValidator()
        {
            RuleFor(ci => ci.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
