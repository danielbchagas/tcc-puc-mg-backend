using System;

namespace ECommerce.Identity.Api.Descriptors.Request
{
    public class DocumentRequest
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid UserId { get; set; }
    }
}
