using System;

namespace ECommerce.Identity.Api.Models.Request
{
    public class EmailRequest
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
