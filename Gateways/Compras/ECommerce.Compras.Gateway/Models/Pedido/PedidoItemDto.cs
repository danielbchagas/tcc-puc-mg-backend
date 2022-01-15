namespace ECommerce.Compras.Gateway.Models.Pedido
{
    public class PedidoItemDto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
    }
}
