using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Carrinho.Domain.Models
{
    public class ItemCarrinho : Entity
    {
        public ItemCarrinho(string nome, int quantidade, decimal valor, string imagem, Guid produtoId, Guid carrinhoId)
        {
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
            Imagem = imagem;
            ProdutoId = produtoId;
            CarrinhoId = carrinhoId;
        }

        #region Propriedades
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid ProdutoId { get; set; }
        public Guid CarrinhoId { get; set; }

        [JsonIgnore]
        public Carrinho Carrinho { get; set; }
        #endregion

        #region Métodos
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
                .ExclusiveBetween(-1, 6)
                .WithMessage(item => $"A quantidade mínima do {item.Nome} é 1 e o máxima do {item.Nome} é 5.");

            RuleFor(ic => ic.Valor)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");
        }
    }
}
