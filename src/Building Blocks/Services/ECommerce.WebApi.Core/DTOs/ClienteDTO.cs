using System;

namespace ECommerce.WebApi.Core.DTOs
{
    public class ClienteDTO
    {
        public ClienteDTO(Guid id, string nome, string sobrenome, string documento, string telefone, string email)
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
