using System;

namespace ECommerce.Identity.Api.Models
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid CustomerId { get; set; }
    }
}
