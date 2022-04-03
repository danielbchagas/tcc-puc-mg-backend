using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Ordering.Gateway.DTOs.Request
{
    public class UpdateBasketRequest
    {
        [Required(ErrorMessage = "O campo identificador é obrigatório.")]
        public Guid Id { get; set; }
        public bool IsEnded { get; set; }
        [Required(ErrorMessage = "O campo identificador do consumidor é obrigatório.")]
        public Guid CustomerId { get; set; }
    }
}
