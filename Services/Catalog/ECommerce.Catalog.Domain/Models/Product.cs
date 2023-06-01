using ECommerce.Catalog.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Catalog.Domain.Models
{
    public class Product : IAggregateRoot, IAuditable
    {
        public Product(Guid id, string description, string name, string image, int quantity, decimal value)
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ValidationResult Validate()
        {
            return new ProductValidator().Validate(this);
        }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Image)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Quantity)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um value menor do que o esperado!")
                .LessThan(int.MaxValue)
                .WithMessage("{PropertyName} tem um value maior do que o esperado!");
            RuleFor(p => p.Value)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um value menor do que o esperado!")
                .LessThan(decimal.MaxValue)
                .WithMessage("{PropertyName} tem um value maior do que o esperado!");
        }
    }
}
