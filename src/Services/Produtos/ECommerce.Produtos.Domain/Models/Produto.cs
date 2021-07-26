using ECommerce.Produtos.Domain.Interfaces.Entities;
using System;

namespace ECommerce.Produtos.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(Guid id, string marca, string nome, string lote, DateTime fabricacao, DateTime vencimento, string observacao, long quantidade, bool ativo = true)
        {
            Id = id;
            Marca = marca;
            Nome = nome;
            Lote = lote;
            Fabricacao = fabricacao;
            Vencimento = vencimento;
            Observacao = observacao;
            Quantidade = quantidade;
            Ativo = ativo;
        }

        public string Marca { get; private set; }
        public string Nome { get; private set; }
        public string Lote { get; private set; }
        public DateTime Fabricacao { get; private set; }
        public DateTime Vencimento { get; private set; }
        public string Observacao { get; private set; }
        public long Quantidade { get; private set; }
        public bool Ativo { get; private set; }

        public void Adicionar(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void Remover(int quantidade)
        {
            Quantidade -= quantidade;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
