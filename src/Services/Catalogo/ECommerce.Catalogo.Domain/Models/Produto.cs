using ECommerce.Catalogo.Domain.Interfaces.Entities;
using FluentValidation;
using System;

namespace ECommerce.Catalogo.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(string descricao, string nome, string imagem, long quantidadeEstoque, decimal preco, bool ativo = true)
        {
            Descricao = descricao;
            Nome = nome;
            Imagem = imagem;
            QuantidadeEstoque = quantidadeEstoque;
            Preco = preco;
            Ativo = ativo;
            DataCadastro = DateTime.Now;
        }

        public string Descricao { get; private set; }
        public string Nome { get; private set; }
        public string Imagem { get; private set; }
        public long QuantidadeEstoque { get; private set; }
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
        
        public void Adicionar(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public void Remover(int quantidade)
        {
            QuantidadeEstoque -= quantidade;
        }

        public void AlterarPreco(decimal novoPreco)
        {
            Preco = novoPreco;
        }
    }

    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Descricao)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Imagem)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.QuantidadeEstoque)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um valor menor do que o esperado!")
                .LessThan(long.MaxValue)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
            RuleFor(p => p.Preco)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um valor menor do que o esperado!")
                .LessThan(decimal.MaxValue)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}
