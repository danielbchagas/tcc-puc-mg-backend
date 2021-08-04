using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Notifications
{
    public class DocumentoCommitNotification : INotification
    {
        public DocumentoCommitNotification(Guid documentoId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            DocumentoId = documentoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid DocumentoId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
