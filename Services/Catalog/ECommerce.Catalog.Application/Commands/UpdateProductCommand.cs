using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<ValidationResult>
    {
        public UpdateProductCommand(Guid id, string description, string name, string image, int quantity, decimal value)
        {
            Id = id;
            Description = description;
            Name = name;
            Image = image;
            Quantity = quantity;
            Value = value;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
