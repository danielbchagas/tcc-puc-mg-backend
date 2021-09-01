using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Carrinho.Api.Models
{
    public class Carrinho
    {
        public Carrinho()
        {
        }

        public Carrinho(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            Itens = new List<ItemCarrinho>();
            Validacao = new CarrinhoValidator();
        }

        internal const int MAX_QUANTIDADE_ITEM = 5;
        private CarrinhoValidator Validacao { get; }

        public Guid Id { get; set; }
        public decimal ValorTotal { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; }

        #region Métodos auxiliares
        internal ValidationResult AtualizarItem(ItemCarrinho item)
        {
            if (ItemExiste(item.Id))
            {
                var itemAntigo = Itens.First(i => i.ProdutoId == item.ProdutoId);
                itemAntigo.AtualizarQuantidade(item.Quantidade);

                item = itemAntigo;

                Itens.Remove(itemAntigo);
            }
            else
                item.AssociarCarrinho(Id);

            if (!item.ValidacaoObjeto().IsValid) 
                return item.ValidacaoObjeto();

            Itens.Add(item);

            ValorTotal += Itens.Sum(i => i.CalcularValor());

            return ValidacaoObjeto();
        }

        internal void ExcluirItem(Guid itemId)
        {
            if (!ItemExiste(itemId)) return;

            Itens.Remove(Itens.First(ic => ic.ProdutoId == itemId));
        }

        internal bool ItemExiste(Guid itemId)
        {
            return Itens.Any(i => i.ProdutoId == itemId);
        }

        internal ValidationResult ValidacaoObjeto()
        {
            return Validacao.Validate(this);
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
