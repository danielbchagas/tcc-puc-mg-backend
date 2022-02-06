using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands
{
    public class UpdateProductCommand : IRequest<ValidationResult>
    {
        public UpdateProductCommand(Guid id, string description, string name, string image, long quantity, decimal value, bool enabled)
        {
            Id = id;
            Description = description;
            Name = name;
            Image = image;
            Quantity = quantity;
            Value = value;
            Enabled = enabled;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public long Quantity { get; set; }
        public decimal Value { get; set; }
        public bool Enabled { get; set; }
    }
}
