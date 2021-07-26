using System;

namespace ECommerce.Clientes.Domain.Models
{
    public class LogEvento : Entity
    {
        public LogEvento(Guid id, string origemRequisicao, DateTime momento, string uri, Guid clienteId)
        {
            Id = id;
            OrigemRequisicao = origemRequisicao;
            Momento = momento;
            Uri = uri;
            ClienteId = clienteId;
        }

        public string OrigemRequisicao { get; private set; }
        public DateTime Momento { get; private set; }
        public string Uri { get; private set; }
        public Guid ClienteId { get; private set; }
    }
}
