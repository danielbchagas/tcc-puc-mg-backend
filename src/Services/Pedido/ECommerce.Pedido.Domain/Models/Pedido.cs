using ECommerce.Pedido.Domain.Enums;
using System.Collections.Generic;

namespace ECommerce.Pedido.Domain.Models
{
    public class Pedido : Entity
    {
        public Pedido(decimal valor, StatusPedido status)
        {
            Valor = valor;
            Status = status;
        }

        public Pedido(decimal valor, StatusPedido status, Cliente cliente, IList<Produto> produtos)
        {
            Valor = valor;
            Status = status;

            Cliente = cliente;
            Produtos = produtos;
        }
        
        #region Propriedades
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        #endregion
    }
}
