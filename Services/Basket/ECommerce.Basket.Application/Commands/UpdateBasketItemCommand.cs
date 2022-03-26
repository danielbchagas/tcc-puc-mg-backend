using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class UpdateBasketItemCommand : IRequest<ValidationResult>
    {
        public UpdateBasketItemCommand(Guid id, string name, int quantity, decimal value, string image, Guid productId, Guid shoppingBasketId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Value = value;
            Image = image;
            ProductId = productId;
            ShoppingBasketId = shoppingBasketId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShoppingBasketId { get; set; }
    }
}
