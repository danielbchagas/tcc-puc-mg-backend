using ECommerce.Cliente.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Notifications
{
    public class ClienteCommitNotificationHandler : INotificationHandler<ClienteCommitNotification>
    {
        public ClienteCommitNotificationHandler()
        {
            
        }

        public Task Handle(ClienteCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
