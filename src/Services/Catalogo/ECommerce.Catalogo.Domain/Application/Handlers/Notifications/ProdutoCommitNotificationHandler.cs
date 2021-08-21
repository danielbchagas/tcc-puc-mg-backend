using ECommerce.Catalogo.Domain.Application.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Catalogo.Domain.Application.Handlers.Notifications
{
    public class ProdutoCommitNotificationHandler : INotificationHandler<ProdutoCommitNotification>
    {
        public ProdutoCommitNotificationHandler()
        {
            
        }

        public Task Handle(ProdutoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
