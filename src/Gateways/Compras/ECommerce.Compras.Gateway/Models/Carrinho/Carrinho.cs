using System;
using System.Collections.Generic;

namespace ECommerce.Compras.Gateway.Models.Carrinho
{
    public class Carrinho
    {
        public Guid Id { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; }
    }
}
