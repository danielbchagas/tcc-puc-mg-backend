using System;

namespace ECommerce.Identity.Api.DTOs.Request
{
    public class DocumentRequest
    {
        public string Number { get; set; }
        public Guid UserId { get; set; }
    }
}
