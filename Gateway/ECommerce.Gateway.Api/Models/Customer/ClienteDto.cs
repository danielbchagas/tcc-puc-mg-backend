using System;

namespace ECommerce.Gateway.Api.Models.Cliente
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }

        public DocumentoDto Documento { get; set; }
        public EmailDto Email { get; set; }
        public TelefoneDto Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
