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

        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; }

        #region Métodos auxiliares
        public ValidationResult AtualizarItem(ItemCarrinho item)
        {
            // Valida se o item é válido
            var itemValido = item.Validar();

            if (!itemValido.IsValid)
            {
                return itemValido;
            }

            // Verifica se o item já existe no carrinho
            // Soma o item existente
            if (ItemExiste(item.ProdutoId))
            {
                var itemAntigo = Itens.First(i => i.ProdutoId == item.ProdutoId);
                itemAntigo.AtualizarQuantidade(item.Quantidade);

                item = itemAntigo;

                Itens.Remove(itemAntigo);
            }
            // Associa o novo item ao carrinho
            else
            {
                item.AssociarCarrinho(Id);
            }

            Itens.Add(item);

            ValorTotal += Itens.Sum(i => i.CalcularValor());

            return Validar();
        }

        public void ExcluirItem(Guid itemId)
        {
            if (!ItemExiste(itemId)) return;

            Itens.Remove(Itens.First(ic => ic.ProdutoId == itemId));
        }

        public bool ItemExiste(Guid itemId)
        {
            return Itens.Any(i => i.ProdutoId == itemId);
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

            RuleFor(ci => ci.ValorTotal)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
