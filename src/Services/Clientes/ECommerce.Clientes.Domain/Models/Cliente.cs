using ECommerce.Clientes.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(Guid id, string nomeFantasia, string cnpj, Endereco endereco, bool ativo = true)
        {
            Id = id;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            Endereco = endereco;
            Ativo = ativo;
        }

        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public Endereco Endereco { get; private set; }
        public bool Ativo { get; private set; }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
