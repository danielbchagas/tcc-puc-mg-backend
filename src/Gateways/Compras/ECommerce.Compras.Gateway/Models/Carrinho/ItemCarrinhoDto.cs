using System;

namespace ECommerce.Compras.Gateway.Models.Carrinho
{
    public class ItemCarrinhoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }
    }
}
