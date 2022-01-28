using System;

namespace ECommerce.Gateway.Api.Models
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerBasketId { get; set; }
    }
}
