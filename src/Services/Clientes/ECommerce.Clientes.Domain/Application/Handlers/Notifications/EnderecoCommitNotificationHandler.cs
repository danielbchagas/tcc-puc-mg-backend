using ECommerce.Clientes.Domain.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Notifications
{
    public class EnderecoCommitNotificationHandler : INotificationHandler<EnderecoCommitNotification>
    {
        public Task Handle(EnderecoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
