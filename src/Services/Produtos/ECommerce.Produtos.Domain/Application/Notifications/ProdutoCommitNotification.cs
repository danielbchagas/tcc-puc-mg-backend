using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Produtos.Domain.Application.Notifications
{
    public class ProdutoCommitNotification : INotification
    {
        public ProdutoCommitNotification(string caminhoRequisicao, string uri, Guid produtoId)
        {
            Momento = DateTime.Now;
            CaminhoRequisicao = caminhoRequisicao;
            Uri = uri;
            ProdutoId = produtoId;

        }

        public DateTime Momento { get; private set; }
        public string CaminhoRequisicao { get; private set; }
        public string Uri { get; private set; }
        public Guid ProdutoId { get; private set; }
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
