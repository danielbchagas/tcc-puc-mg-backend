using System.Collections.Generic;
using ECommerce.Ordering.Domain.Enums;
using ECommerce.Ordering.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Ordering.Application.Commands
{
    public class CreateOrderCommand : IRequest<ValidationResult>
    {
        public CreateOrderCommand(string fullName, string document, string phone, string email, decimal valor, string firstLine, string secondLine, string city, State state, string zipCode)
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

            Value = valor;
        }

        public string FullName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }

        public decimal Value { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
