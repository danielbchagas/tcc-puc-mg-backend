using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Ordering.Gateway.DTOs.Request
{
    public class BasketRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo identificador do consumidor é obrigatório.")]
        public Guid CustomerId { get; set; }
    }
}
