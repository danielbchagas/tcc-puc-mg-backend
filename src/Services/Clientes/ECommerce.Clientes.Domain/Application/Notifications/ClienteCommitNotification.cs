using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Notifications
{
    public class ClienteCommitNotification : INotification
    {
        public ClienteCommitNotification(string caminhoRequisicao, string uri, Guid clienteId)
        {
            Momento = DateTime.Now;
            CaminhoRequisicao = caminhoRequisicao;
            Uri = uri;
            ProdutoId = clienteId;

        }

        public DateTime Momento { get; private set; }
        public string CaminhoRequisicao { get; private set; }
        public string Uri { get; private set; }
        public Guid ProdutoId { get; private set; }
    }
}
