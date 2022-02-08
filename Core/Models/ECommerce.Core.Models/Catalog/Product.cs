using ECommerce.Core.Contracts.Entity;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Core.Models.Catalog
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string description, string name, string image, long quantity, decimal value, bool enabled = true)
        {
            Description = description;
            Name = name;
            Image = image;
            Quantity = quantity;
            Value = value;
            Enabled = enabled;
            RegistrationDate = DateTime.Now;
        }

        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public long Quantity { get; set; }
        public decimal Value { get; set; }
        public bool Enabled { get; set; }
        public DateTime RegistrationDate { get; set; }

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
                .LessThan(long.MaxValue)
                .WithMessage("{PropertyName} tem um value maior do que o esperado!");
            RuleFor(p => p.Value)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um value menor do que o esperado!")
                .LessThan(decimal.MaxValue)
                .WithMessage("{PropertyName} tem um value maior do que o esperado!");
        }
    }
}
