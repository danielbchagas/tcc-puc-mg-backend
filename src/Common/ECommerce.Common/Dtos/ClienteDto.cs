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

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Documento { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
    }
}
