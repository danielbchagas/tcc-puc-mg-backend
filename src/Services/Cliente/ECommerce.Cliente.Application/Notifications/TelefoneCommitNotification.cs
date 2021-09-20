using MediatR;
using System;

namespace ECommerce.Cliente.Application.Notifications
{
    public class TelefoneCommitNotification : INotification
    {
        public TelefoneCommitNotification(Guid telefoneId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            TelefoneId = telefoneId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid TelefoneId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
