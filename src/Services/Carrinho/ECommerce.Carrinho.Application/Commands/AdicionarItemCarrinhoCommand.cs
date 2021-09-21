using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class AdicionarItemCarrinhoCommand : IRequest<ValidationResult>
    {
        public AdicionarItemCarrinhoCommand(Guid id, string nome, int quantidade, decimal valor, string imagem, Guid produtoId, Guid carrinhoId, Guid clienteId)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
            Imagem = imagem;

            ProdutoId = produtoId;
            CarrinhoId = carrinhoId;
            ClienteId = clienteId;
    }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ClienteId { get; set; }
    }
}
