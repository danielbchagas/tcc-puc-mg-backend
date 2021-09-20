using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class AtualizarItemCarrinhoCommand : IRequest<ValidationResult>
    {
        public AtualizarItemCarrinhoCommand(Guid id, string nome, int quantidade, decimal valor, string imagem, Guid produtoId)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
            Imagem = imagem;
            ProdutoId = produtoId;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
    }
}
