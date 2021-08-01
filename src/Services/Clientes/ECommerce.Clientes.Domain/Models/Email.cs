using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Email : Entity
    {
        protected Email()
        {

        }

        public Email(string endereco, Guid clienteId)
        {
            Endereco = endereco;
            ClienteId = clienteId;
        }

        public string Endereco { get; private set; }
        
        // Relacionamentos
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
    }
}
