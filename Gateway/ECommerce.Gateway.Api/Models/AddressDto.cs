using System;
using ECommerce.Gateway.Api.Enums;

namespace ECommerce.Gateway.Api.Models
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string ZipCode { get; set; }
        public Guid CustomerId { get; set; }
    }
}