using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Produto : Entity
    {
        public Produto(string nome, string imagem, decimal valor, int quantidade, Guid pedidoId)
        {
            Nome = nome;
            Imagem = imagem;
            Valor = valor;
            Quantidade = quantidade;

            PedidoId = pedidoId;
        }
        
        #region Propriedades
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        #endregion
    }
}
