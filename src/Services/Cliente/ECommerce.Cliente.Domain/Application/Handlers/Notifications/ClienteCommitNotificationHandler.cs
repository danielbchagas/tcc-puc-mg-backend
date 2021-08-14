using ECommerce.Cliente.Domain.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
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
