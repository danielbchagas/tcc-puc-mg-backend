using ECommerce.Compras.Gateway.Enums;
using System.Collections.Generic;

namespace ECommerce.Compras.Gateway.Models.Pedido
{
    public class PedidoDto
    {
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public ClienteDto Cliente { get; set; }
        public ICollection<PedidoItemDto> Produtos { get; set; }
    }
}
