namespace ECommerce.Compras.Gateway.Models.Pedido
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public DocumentoDto Documento { get; set; }
        public EmailDto Email { get; set; }
        public TelefoneDto Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
