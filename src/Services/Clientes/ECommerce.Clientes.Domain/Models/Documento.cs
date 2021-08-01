using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Documento : Entity
    {
        protected Documento()
        {

        }

        public Documento(string numero, Guid clienteId, bool ativo = true)
        {
            Numero = numero;
            Ativo = ativo;
            ClienteId = clienteId;
        }

        public string Numero { get; private set; }
        public bool Ativo { get; private set; }

        // Relacionamentos
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }

        // Métodos auxiliares
        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
