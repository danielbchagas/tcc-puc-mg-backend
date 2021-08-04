using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Notifications
{
    public class EmailCommitNotification : INotification
    {
        public EmailCommitNotification(Guid emailId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            EmailId = emailId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid EmailId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
