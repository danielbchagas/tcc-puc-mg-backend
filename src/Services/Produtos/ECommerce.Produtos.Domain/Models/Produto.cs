using ECommerce.Common.Interfaces.Entities;
using ECommerce.Common.Models;
using System;

namespace ECommerce.Produtos.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(Guid id, string marca, string nome, string lote, DateTime fabricacao, DateTime vencimento, string observacao, long quantidade)
        {
            Id = id;
            Marca = marca;
            Nome = nome;
            Lote = lote;
            Fabricacao = fabricacao;
            Vencimento = vencimento;
            Observacao = observacao;
            Quantidade = quantidade;
        }

        public string Marca { get; private set; }
        public string Nome { get; private set; }
        public string Lote { get; private set; }
        public DateTime Fabricacao { get; private set; }
        public DateTime Vencimento { get; private set; }
        public string Observacao { get; private set; }
        public long Quantidade { get; private set; }

        public void Adicionar(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void Remover(int quantidade)
        {
            Quantidade -= quantidade;
        }
    }
}
