using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Ordering.Gateway.DTOs.Request
{
    public class OrderingRequest
    {
        [Required(ErrorMessage = "O campo identificador do usuário é obrigatório.")]
        public Guid CustomerId { get; set; }
        [Required(ErrorMessage = "O campo identificador do carrinho é obrigatório.")]
        public Guid ShoppingBasketId { get; set; }
    }
}
