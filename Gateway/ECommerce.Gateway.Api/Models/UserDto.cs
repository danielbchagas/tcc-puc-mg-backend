using System;

namespace ECommerce.Gateway.Api.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }

        public DocumentDto Document { get; set; }
        public EmailDto Email { get; set; }
        public PhoneDto Phone { get; set; }
        public AddressDto Address { get; set; }
    }
}
