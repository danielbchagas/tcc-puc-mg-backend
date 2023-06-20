using System;

namespace ECommerce.Identity.Api.Models.Request
{
    public class AddressRequest
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid CustomerId { get; set; }
    }
}
