using System;
using MediatR;

namespace ECommerce.Cliente.Application.Notifications
{
    public class EnderecoCommitNotification : INotification
    {
        public EnderecoCommitNotification(Guid enderecoId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            EnderecoId = enderecoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid EnderecoId { get; private set; }
        public Guid UsuarioId { get; private set; }
    }
}
