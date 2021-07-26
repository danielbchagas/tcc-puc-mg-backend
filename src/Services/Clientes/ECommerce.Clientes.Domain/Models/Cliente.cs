using ECommerce.Clientes.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente()
        {

        }

        public Cliente(Guid id, string nomeFantasia, string cnpj, bool ativo = true)
        {
            Id = id;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            Ativo = ativo;
        }

        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public bool Ativo { get; private set; }

        public virtual Endereco Endereco { get; set; }

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
