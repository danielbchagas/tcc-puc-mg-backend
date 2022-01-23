using ECommerce.Compras.Gateway.Enums;

namespace ECommerce.Compras.Gateway.Models.Pedido
{
    public class EnderecoDto
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
    }
}
