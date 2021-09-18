using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Carrinho.Domain.Models
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
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }

        [JsonIgnore]
        public Carrinho Carrinho { get; set; }

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

        public ValidationResult Validar()
        {
            return new ItemCarrinhoValidator().Validate(this);
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
