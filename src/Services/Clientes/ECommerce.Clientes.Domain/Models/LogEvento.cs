using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class LogEvento : Entity
    {
        protected LogEvento()
        {

        }

        public LogEvento(Guid clienteId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            ClienteId = clienteId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
