using System;

namespace ECommerce.Compras.Gateway.Dtos.Carrinho
{
    public class BuscarCarrinhoPorClienteDto
    {
        public BuscarCarrinhoPorClienteDto(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public Guid ClienteId { get; set; }
    }
}
