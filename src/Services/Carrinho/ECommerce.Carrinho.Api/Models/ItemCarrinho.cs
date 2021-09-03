using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Carrinho.Api.Models
{
    public class ItemCarrinho
    {
        public ItemCarrinho(string nome, int quantidade, decimal valor, string imagem, Guid produtoId, Guid carrinhoId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
            Imagem = imagem;
            ProdutoId = produtoId;
            CarrinhoId = carrinhoId;

            Validacao = new ItemCarrinhoValidator().Validate(this);
        }

        public ValidationResult Validacao { get; private set; }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Valor { get; private set; }
        public string Imagem { get; private set; }

        public Guid ProdutoId { get; private set; }
        public Guid CarrinhoId { get; private set; }

        [JsonIgnore]
        public Carrinho CarrinhoCliente { get; private set; }

        #region Métodos auxiliares
        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * Valor;
        }

        internal void AtualizarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
        #endregion
    }

    public class ItemCarrinhoValidator : AbstractValidator<ItemCarrinho>
    {
        public ItemCarrinhoValidator()
        {
            RuleFor(ic => ic.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(ic => ic.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(ic => ic.Quantidade)
                .ExclusiveBetween(-1, Carrinho.MAX_QUANTIDADE_ITEM + 1)
                .WithMessage(item => $"A quantidade máxima do {item.Nome} é {Carrinho.MAX_QUANTIDADE_ITEM}");

            RuleFor(ic => ic.Valor)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");
        }
    }
}
