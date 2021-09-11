using System;

namespace ECommerce.Compras.Gateway.Models.Catalogo
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public long QuantidadeEstoque { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
