using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Notifications
{
    public class EnderecoCommitNotification : INotification
    {
        public EnderecoCommitNotification(string caminhoRequisicao, string uri, Guid clienteId)
        {
            Momento = DateTime.Now;
            CaminhoRequisicao = caminhoRequisicao;
            Uri = uri;
            ClienteId = clienteId;

        }

        public DateTime Momento { get; private set; }
        public string CaminhoRequisicao { get; private set; }
        public string Uri { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
