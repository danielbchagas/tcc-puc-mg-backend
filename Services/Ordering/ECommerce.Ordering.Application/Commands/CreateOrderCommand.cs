using ECommerce.Ordering.Domain.Models;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Ordering.Application.Commands
{
    public class CreateOrderCommand : IRequest<ValidationResult>
    {
        public CreateOrderCommand(Guid id, string fullName, string document, string phone, string email, decimal valor, string firstLine, string secondLine, string city, string state, string zipCode)
        {
            Id = id;
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

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public decimal Value { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
