using System;

namespace ECommerce.Gateway.Domain.Models
{
    public class CustomerBasket
    {
        public CustomerBasket(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
