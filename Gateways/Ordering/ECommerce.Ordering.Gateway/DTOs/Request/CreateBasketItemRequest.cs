using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Ordering.Gateway.DTOs.Request
{
    public class CreateBasketItemRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo identificador do produto é obrigatório.")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "O campo identificador do carrinho é obrigatório.")]
        public Guid BasketId { get; set; }
        [Required(ErrorMessage = "O campo quantidade é obrigatório.")]
        public int Quantity { get; set; }
    }
}
