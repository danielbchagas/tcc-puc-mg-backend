using System;
using ECommerce.Ordering.Gateway.Enums;

namespace ECommerce.Ordering.Gateway.Models
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }
        public Guid UserId { get; set; }
    }
}