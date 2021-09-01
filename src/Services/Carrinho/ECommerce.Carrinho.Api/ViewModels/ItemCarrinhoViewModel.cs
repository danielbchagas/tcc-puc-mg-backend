using System;

namespace ECommerce.Carrinho.Api.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }
    }
}
