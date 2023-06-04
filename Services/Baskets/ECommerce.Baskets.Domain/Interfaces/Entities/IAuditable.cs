﻿using System;

namespace ECommerce.Baskets.Domain.Interfaces.Entities
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
