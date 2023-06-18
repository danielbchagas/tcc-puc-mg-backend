﻿using System;

namespace ECommerce.Identity.Api.Models.Request
{
    public class CustomerRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public string ZipCode { get; set; }

        public DocumentRequest Document { get; set; }
        public EmailRequest Email { get; set; }
        public PhoneRequest Phone { get; set; }
    }
}
