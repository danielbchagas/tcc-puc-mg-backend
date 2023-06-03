using ECommerce.Basket.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IList<ValidationResult> UpdatesItems(IEnumerable<Item> items)
        {
            var validationResult = new List<ValidationResult>();

            foreach(var item in items)
                validationResult.Add(item.Validate());

            if(validationResult.Any(vr => !vr.IsValid))
                return validationResult;

            Items.Clear();
            
            foreach(var item in items)
                Items.Add(item);

            return validationResult;
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
