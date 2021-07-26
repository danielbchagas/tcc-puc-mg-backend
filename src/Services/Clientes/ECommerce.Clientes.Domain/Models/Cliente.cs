using ECommerce.Clientes.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(Guid id, string nome, string sobrenome, Documento documento, Endereco endereco, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Endereco = endereco;
            Ativo = ativo;
        }

        public string Nome { private get; set; }
        public string Sobrenome { private get; set; }
        public Documento Documento { private get; set; }
        public Endereco Endereco { private get; set; }
        public bool Ativo { private get; set; }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
