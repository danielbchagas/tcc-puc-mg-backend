using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Notifications
{
    public class ProdutoCommitNotification : INotification
    {
        public ProdutoCommitNotification(bool sucesso)
        {
            Momento = DateTime.Now;
            Sucesso = sucesso;
        }

        public DateTime Momento { get; private set; }
        public bool Sucesso { get; private set; }
    }

    public class ProdutoCommitNotificationHandler : INotificationHandler<ProdutoCommitNotification>
    {
        public Task Handle(ProdutoCommitNotification notification, CancellationToken cancellationToken)
        {
            // Logar

            return Task.CompletedTask;
        }
    }
}
