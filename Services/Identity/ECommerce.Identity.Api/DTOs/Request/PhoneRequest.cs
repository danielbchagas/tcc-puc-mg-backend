using System;

namespace ECommerce.Identity.Api.DTOs.Request
{
    public class PhoneRequest
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        public Guid UserId { get; set; }
    }
}
