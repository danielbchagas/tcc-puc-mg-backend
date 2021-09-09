using System;

namespace ECommerce.Identidade.Api.Models
{
    public class ClienteDto
    {
        public ClienteDto(Guid id, string nome, string sobrenome, bool ativo, string documento, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;
            Documento = documento;
            Telefone = telefone;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
