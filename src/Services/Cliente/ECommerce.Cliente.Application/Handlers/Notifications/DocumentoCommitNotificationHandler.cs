using ECommerce.Cliente.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Notifications
{
    public class DocumentoCommitNotificationHandler : INotificationHandler<DocumentoCommitNotification>
    {
        public DocumentoCommitNotificationHandler()
        {
            
        }

        public Task Handle(DocumentoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
