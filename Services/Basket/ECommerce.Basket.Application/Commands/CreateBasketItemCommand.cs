using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class CreateBasketItemCommand : IRequest<ValidationResult>
    {
        public CreateBasketItemCommand(Guid id, string name, int quantity, decimal value, string image, Guid customerBasketId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Value = value;
            Image = image;

            CustomerBasketId = customerBasketId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }

        public Guid CustomerBasketId { get; set; }
    }
}
