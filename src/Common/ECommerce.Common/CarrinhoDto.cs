using System;
using System.Collections.Generic;

namespace ECommerce.Common.Dtos
{
    public class CarrinhoDto
    {
        public Guid Id { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }

        public ICollection<ItemCarrinhoDto> Itens { get; private set; }
    }
}
