using System;

namespace ECommerce.Identity.Api.Models
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }

        public DocumentDto Document { get; set; }
        public EmailDto Email { get; set; }
        public PhoneDto Phone { get; set; }
    }
}
