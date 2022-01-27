using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Commands
{
    public class CreateItemCommand : IRequest<ValidationResult>
    {
        public CreateItemCommand(Guid id, string name, int quantity, decimal value, string image, Guid productId, Guid CartId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Value = value;
            Image = image;

            ProductId = productId;
            this.CartId = CartId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }

        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}
