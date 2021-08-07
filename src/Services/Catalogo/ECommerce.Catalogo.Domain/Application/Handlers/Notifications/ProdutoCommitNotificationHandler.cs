using System.Threading;
using System.Threading.Tasks;
using ECommerce.Catalogo.Domain.Application.Notifications;
using MediatR;

namespace ECommerce.Catalogo.Domain.Application.Handlers.Notifications
{
    public class ProdutoCommitNotificationHandler : INotificationHandler<ProdutoCommitNotification>
    {
        public Task Handle(ProdutoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
