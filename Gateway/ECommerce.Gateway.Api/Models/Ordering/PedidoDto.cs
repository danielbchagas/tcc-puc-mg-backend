using ECommerce.Gateway.Api.Enums;
using ECommerce.Gateway.Api.Models.Cliente;
using System.Collections.Generic;

namespace ECommerce.Gateway.Api.Models.Pedido
{
    public class PedidoDto
    {
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public ClienteDto Cliente { get; set; }
        public ICollection<PedidoItemDto> Produtos { get; set; }
    }
}
