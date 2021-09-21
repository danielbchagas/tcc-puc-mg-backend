using System;

namespace ECommerce.Compras.Gateway.Dtos.Carrinho
{
    public class AdicionarCarrinhoDto
    {
        public AdicionarCarrinhoDto(Guid id, decimal valor, Guid clienteId)
        {
            Id = id;
            Valor = valor;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }

        public Guid ClienteId { get; set; }
    }
}
