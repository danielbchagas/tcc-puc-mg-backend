using System;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Notifications
{
    public class ClienteCommitNotification : INotification
    {
        public ClienteCommitNotification(Guid clienteId, Guid usuarioId)
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
