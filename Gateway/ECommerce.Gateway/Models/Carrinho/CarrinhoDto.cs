using System;

namespace ECommerce.Compras.Gateway.Models.Carrinho
{
    public class CarrinhoDto
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }
    }
}
