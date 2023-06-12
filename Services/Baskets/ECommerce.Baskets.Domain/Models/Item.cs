using ECommerce.Baskets.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Baskets.Domain.Models
{
    public class Item : IAuditable, IEquatable<Item>
    {
        public Item(Guid id, string name, int quantity, decimal value)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Value = value;
        }

        /// <summary>
        /// Same id of product.
        /// </summary>
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid BasketId { get; private set; }

        #region IEquatable
        public bool Equals(Item other)
        {
            if (other is null)
                return false;

            return Id == other.Id
                && Name == other.Name
                && Quantity == other.Quantity
                && Value == other.Value
                && CreatedAt == other.CreatedAt
                && UpdatedAt == other.UpdatedAt
                && DeletedAt == other.DeletedAt
                && BasketId == other.BasketId;
        }

        public override bool Equals(object obj) => Equals(obj as Item);
        public override int GetHashCode() => (Id, Name, Quantity, Value, CreatedAt, UpdatedAt, DeletedAt, BasketId).GetHashCode();
        #endregion

        public void LinkToBasket(Guid basketId) => BasketId = basketId;

        public ValidationResult Validate() => new ItemValidator().Validate(this);
    }

    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(ic => ic.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(ic => ic.Name)
                .NotEmpty()
                .WithMessage("O name do produto não foi informado");

            RuleFor(ic => ic.Quantity)
                .ExclusiveBetween(0, 6)
                .WithMessage(item => $"A quantity mínima do {item.Name} é 1 e o máxima do {item.Name} é 5.");

            RuleFor(ic => ic.Value)
                .GreaterThan(0)
                .WithMessage(item => $"O value do {item.Name} precisa ser maior que 0");
        }
    }
}
