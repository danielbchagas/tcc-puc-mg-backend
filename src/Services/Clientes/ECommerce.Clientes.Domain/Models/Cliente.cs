using ECommerce.Clientes.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente()
        {

        }

        public Cliente(Guid id, string nomeFantasia, string cnpj, Endereco endereco, bool ativo = true)
        {
            Id = id;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            Endereco = endereco;
            Ativo = ativo;
        }

        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }

        public Guid EnderecoId { get; set; }
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
