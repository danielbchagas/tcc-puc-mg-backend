using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Carrinho.Api.Models
{
    public class ItemCarrinho
    {
        public ItemCarrinho()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }
        [JsonIgnore]
        public CarrinhoCliente CarrinhoCliente { get; set; }

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
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o {item.Nome} é 1");

            RuleFor(ic => ic.Quantidade)
                .LessThanOrEqualTo(CarrinhoCliente.MAX_QUANTIDADE_ITEM)
                .WithMessage(item => $"A quantidade máxima do {item.Nome} é {CarrinhoCliente.MAX_QUANTIDADE_ITEM}");

            RuleFor(ic => ic.Valor)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");
        }
    }
}
