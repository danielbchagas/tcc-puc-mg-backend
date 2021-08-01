using ECommerce.Clientes.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        protected Cliente()
        {

        }

        public Cliente(string nome, string sobrenome, DateTime nascimento, bool ativo = true)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Nascimento = nascimento;
            Ativo = ativo;
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime Nascimento { get; set; }
        public bool Ativo { get; private set; }

        // Relacionamento
        public Email Email { get; set; }
        public Documento Documento { get; private set; }
        public Endereco Endereco { get; private set; }

        // Métodos auxiliares
        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void VincularEmail(Email email)
        {
            Email = email;
        }

        public void VincularDocumento(Documento documento)
        {
            Documento = documento;
        }

        public void VincularEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}
