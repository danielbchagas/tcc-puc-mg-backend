using System;

namespace ECommerce.Ordering.Gateway.Models
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
