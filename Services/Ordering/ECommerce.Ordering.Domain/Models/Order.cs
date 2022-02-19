using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Ordering.Domain.Models
{
    public class Order : Entity
    {
        public Order(string fullName, string document, string phone, string email, string firstLine, string secondLine, string city, string state, string zipCode)
        {
            FullName = fullName;
            Document = document;
            Phone = phone;
            Email = email;

            FirstLine = firstLine;
            SecondLine = secondLine;
            City = city;
            State = state;
            ZipCode = zipCode;

            Status = "Processando";
            Items = new List<OrderItem>();
        }

        public string FullName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public decimal Value { get; private set; }
        public string Status { get; set; }
        
        public ICollection<OrderItem> Items { get; set; }

        public ValidationResult Validate()
        {
            return new OrderValidator().Validate(this);
        }

        public void Totalize()
        {
            Value = Items.Sum(_ => _.Quantity * _.Value);
        }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Value)
                .GreaterThan(0)
                .WithMessage("{PopertyName} não pode ser 0!");
            RuleFor(_ => _.Items)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo!");
        }
    }
}
