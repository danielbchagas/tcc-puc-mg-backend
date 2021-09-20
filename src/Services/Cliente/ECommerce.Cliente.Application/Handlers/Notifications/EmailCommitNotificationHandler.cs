using ECommerce.Cliente.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Notifications
{
    public class EmailCommitNotificationHandler : INotificationHandler<EmailCommitNotification>
    {
        public EmailCommitNotificationHandler()
        {
            
        }

        public Task Handle(EmailCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
