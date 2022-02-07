using System;
using System.Collections.Generic;
using ECommerce.Ordering.Gateway.Enums;

namespace ECommerce.Ordering.Gateway.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
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
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
