namespace ECommerce.Pedido.Domain.Models
{
    public class Produto : Entity
    {
        public Produto(string nome, string imagem, decimal valor, int quantidade)
        {
            Nome = nome;
            Imagem = imagem;
            Valor = valor;
            Quantidade = quantidade;
        }
        
        #region Propriedades
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        #endregion
    }
}
