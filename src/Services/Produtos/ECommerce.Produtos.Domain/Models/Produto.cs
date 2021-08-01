using ECommerce.Produtos.Domain.Interfaces.Entities;
using FluentValidation;
using System;

namespace ECommerce.Produtos.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(string marca, string nome, string lote, string imagem, string observacao, long quantidade, DateTime? fabricacao, DateTime? vencimento = null)
        {
            Marca = marca;
            Nome = nome;
            Lote = lote;
            Fabricacao = fabricacao;
            Vencimento = vencimento;
            Imagem = imagem;
            Observacao = observacao;
            Quantidade = quantidade;
        }

        public string Marca { get; private set; }
        public string Nome { get; private set; }
        public string Lote { get; private set; }
        public DateTime? Fabricacao { get; private set; }
        public DateTime? Vencimento { get; private set; }
        public string Imagem { get; private set; }
        public string Observacao { get; private set; }
        public long Quantidade { get; private set; } // Poderia ser ulong
        
        public void Adicionar(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void Remover(int quantidade)
        {
            Quantidade -= quantidade;
        }
    }

    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} é inválido!");
            RuleFor(p => p.Marca).NotNull().NotEmpty().WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(p => p.Nome).NotNull().NotEmpty().WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(p => p.Lote).NotEmpty().WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(p => p.Fabricacao).GreaterThan(DateTime.Now).WithMessage("{PropertyName} maior do que o permitido!");
            RuleFor(p => p.Vencimento).LessThan(DateTime.Now).WithMessage("{PropertyName} menor do que o permitido!");
            RuleFor(p => p.Imagem).NotEmpty().WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(p => p.Observacao).NotEmpty().WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(p => p.Quantidade).LessThan(0).GreaterThan(long.MaxValue).WithMessage("{PropertyName} é inválido!}");
            // Ativo não precisa ser validado
        }
    }
}
