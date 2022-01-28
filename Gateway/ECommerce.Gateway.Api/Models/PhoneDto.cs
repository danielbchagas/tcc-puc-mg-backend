using System;

namespace ECommerce.Gateway.Api.Models
{
    public class PhoneDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }
}
