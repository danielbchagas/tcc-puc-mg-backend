using ECommerce.Cliente.Domain.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Cliente.Domain.Application.Handlers.Notifications
{
    public class EnderecoCommitNotificationHandler : INotificationHandler<EnderecoCommitNotification>
    {
        public EnderecoCommitNotificationHandler()
        {
            
        }

        public Task Handle(EnderecoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
