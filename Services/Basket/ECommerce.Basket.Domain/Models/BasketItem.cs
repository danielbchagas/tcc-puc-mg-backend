using System;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Basket.Domain.Models
{
    public class BasketItem : Entity
    {
        public BasketItem(string name, int quantity, decimal value, string image, Guid productId, Guid customerBasketId)
        {
            Name = name;
            Quantity = quantity;
            Value = value;
            Image = image;
            ProductId = productId;
            CustomerBasketId = customerBasketId;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }

        public Guid ProductId { get; set; }
        public Guid CustomerBasketId { get; set; }

        [JsonIgnore]
        public CustomerBasket CustomerBasket { get; set; }

        public ValidationResult Validate()
        {
            return new ItemValidator().Validate(this);
        }
    }

    public class ItemValidator : AbstractValidator<BasketItem>
    {
        public ItemValidator()
        {
            RuleFor(ic => ic.ProductId)
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
