using ECommerce.Common.Models;
using System;

namespace ECommerce.Produtos.Domain.Models
{
    public class LogEvento : Entity
    {
        public LogEvento(Guid id, string origemRequisicao, DateTime momento, string uri, Guid produtoId)
        {
            Id = id;
            OrigemRequisicao = origemRequisicao;
            Momento = momento;
            Uri = uri;
            ProdutoId = produtoId;
        }

        public string OrigemRequisicao { get; private set; }
        public DateTime Momento { get; private set; }
        public string Uri { get; private set; }
        public Guid ProdutoId { get; private set; }
    }
}
