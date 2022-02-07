using System;

namespace ECommerce.Ordering.Gateway.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public long Quantity { get; set; }
        public decimal Value { get; set; }
        public bool Enabled { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
