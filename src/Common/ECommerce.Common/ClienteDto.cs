using System;

namespace ECommerce.Common.Dtos
{
    public class ClienteDto
    {
        public ClienteDto(Guid id, string nome, string sobrenome, string documento, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Telefone = telefone;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
