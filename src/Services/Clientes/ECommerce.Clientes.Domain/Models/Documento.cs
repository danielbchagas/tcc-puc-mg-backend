using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class Documento : Entity
    {
        protected Documento()
        {

        }

        public Documento(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }

        public string Numero { get; private set; }
        
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
    }
}
