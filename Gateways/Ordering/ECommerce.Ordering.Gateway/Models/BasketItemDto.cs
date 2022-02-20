using System;

namespace ECommerce.Ordering.Gateway.Models
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public int Quantity { get; set; }
    }
}
