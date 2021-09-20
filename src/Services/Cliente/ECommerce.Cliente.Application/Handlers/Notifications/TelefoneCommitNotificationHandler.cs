using ECommerce.Cliente.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Application.Handlers.Notifications
{
    public class TelefoneCommitNotificationHandler : INotificationHandler<TelefoneCommitNotification>
    {
        public TelefoneCommitNotificationHandler()
        {
            
        }

        public Task Handle(TelefoneCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
