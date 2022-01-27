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
            Itens = new List<BasketItem>();
        }

        public decimal Value { get; set; }
        public Guid CustomerId { get; set; }

        public ICollection<BasketItem> Itens { get; set; }

        public ValidationResult AddItens(BasketItem item)
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
