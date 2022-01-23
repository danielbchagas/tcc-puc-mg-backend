using ECommerce.Compras.Gateway.Enums;

namespace ECommerce.Compras.Gateway.Models.Pagamento.Request
{
    public class CartaoDto
    {
        public string Documento { get; set; }
        public string Numero { get; set; }
        public string Vencimento { get; set; }
        public string CodigoSeguranca { get; set; }
        public BandeiraCartao Bandeira { get; set; }
        public decimal Valor { get; set; }
    }
}
