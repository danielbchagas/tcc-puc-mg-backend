using System;
using ECommerce.Catalogo.Domain.Enums;
using ECommerce.Catalogo.Domain.Interfaces.Entities;
using FluentValidation;

namespace ECommerce.Catalogo.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(string marca, string nome, string lote, string imagem, string observacao, long quantidade, decimal preco, bool ativo = true)
        {
            Marca = marca;
            Nome = nome;
            Lote = lote;
            Imagem = imagem;
            Observacao = observacao;
            Quantidade = quantidade;
            Preco = preco;
            Ativo = ativo;
        }

        public string Marca { get; private set; }
        public string Nome { get; private set; }
        public string Lote { get; private set; }
        public string Imagem { get; private set; }
        public string Observacao { get; private set; }
        public long Quantidade { get; private set; } // Poderia ser ulong
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }

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
            Quantidade += quantidade;
        }

        public void Remover(int quantidade)
        {
            Quantidade -= quantidade;
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
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Marca)
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Lote)
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Imagem)
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Observacao)
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(p => p.Quantidade)
                .LessThan(0)
                .WithMessage(ErrosValidacao.MenorQue.ToString())
                .GreaterThan(long.MaxValue)
                .WithMessage(ErrosValidacao.MenorQue.ToString());
            RuleFor(p => p.Preco)
                .LessThan(0)
                .WithMessage(ErrosValidacao.MenorQue.ToString())
                .GreaterThan(decimal.MaxValue)
                .WithMessage(ErrosValidacao.MaiorQue.ToString());
        }
    }
}
