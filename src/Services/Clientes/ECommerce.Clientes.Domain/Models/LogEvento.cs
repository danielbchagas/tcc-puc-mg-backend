using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class LogEvento : Entity
    {
        protected LogEvento()
        {

        }

        public LogEvento(Guid entidadeId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            EntidadeId = entidadeId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid EntidadeId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
