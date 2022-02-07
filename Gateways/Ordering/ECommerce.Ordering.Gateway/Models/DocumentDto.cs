using System;

namespace ECommerce.Ordering.Gateway.Models
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid UserId { get; set; }
    }
}
