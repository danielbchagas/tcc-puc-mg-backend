﻿using System;

namespace ECommerce.Identity.Api.Models
{
    public class DocumentDto
    {
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }
}