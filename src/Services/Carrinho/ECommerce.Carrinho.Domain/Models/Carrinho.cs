using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Carrinho.Domain.Models
{
    public class Carrinho
    {
        protected Carrinho() { }

        public Carrinho(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            Itens = new List<ItemCarrinho>();
        }

        #region Propriedades
        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; }
        #endregion

        #region Métodos
        public ValidationResult AtualizarItensCarrinho(ItemCarrinho item)
        {
            // Valida se o item é válido
            var validationResult = item.Validar();

            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            // Verifica se o item já existe no carrinho
            // Soma o item existente
            if (Itens.Any(i => i.ProdutoId == item.ProdutoId))
            {
                var itemAntigo = Itens.First(i => i.ProdutoId == item.ProdutoId);
                itemAntigo.Quantidade = item.Quantidade;

                item = itemAntigo;

                Itens.Remove(itemAntigo);
            }
            
            Itens.Add(item);

            Valor += Itens.Sum(i => i.Quantidade * i.Valor);

            return Validar();
        }

        public ValidationResult Validar()
        {
            return new CarrinhoValidator().Validate(this);
        }
        #endregion
    }

    public class CarrinhoValidator : AbstractValidator<Carrinho>
    {
        public CarrinhoValidator()
        {
            RuleFor(ci => ci.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");

            RuleFor(ci => ci.Itens.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(ci => ci.Valor)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
