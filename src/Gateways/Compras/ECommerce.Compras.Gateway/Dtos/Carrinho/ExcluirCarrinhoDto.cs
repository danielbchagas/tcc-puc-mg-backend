using System;

namespace ECommerce.Compras.Gateway.Dtos.Carrinho
{
    public class ExcluirCarrinhoDto
    {
        public ExcluirCarrinhoDto(Guid id, Guid clienteId)
        {
            Id = id;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }
    }
}
