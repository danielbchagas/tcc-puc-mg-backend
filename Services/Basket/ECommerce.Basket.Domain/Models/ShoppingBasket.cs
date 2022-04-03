using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Basket.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Basket.Domain.Models
{
    public class ShoppingBasket : IAggregateRoot
    {
        #region Constructor
        public ShoppingBasket(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
            IsEnded = false;
            RegistrationDate = DateTime.Now;
            Items = new List<BasketItem>();
        }
        #endregion

        #region Property
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsEnded { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<BasketItem> Items { get; set; }
        #endregion

        #region Method
        public ValidationResult UpdatesItems(BasketItem item)
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

        public ValidationResult Finalize()
        {
            IsEnded = true;

            return Validate();
        }
        #endregion
    }

    public class ShoppingBasketValidator : AbstractValidator<ShoppingBasket>
    {
        public ShoppingBasketValidator()
        {
            RuleFor(ci => ci.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
