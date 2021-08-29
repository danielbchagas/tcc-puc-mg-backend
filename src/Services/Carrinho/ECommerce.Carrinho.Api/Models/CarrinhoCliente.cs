using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Carrinho.Api.Models
{
    public class CarrinhoCliente
    {
        public CarrinhoCliente()
        {
        }

        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            Itens = new List<ItemCarrinho>();
        }

        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public ICollection<ItemCarrinho> Itens { get; set; }

        internal void AdicionarItem(ItemCarrinho item)
        {
            item.AssociarCarrinho(Id);

            if (ItemExiste(item))
            {
                var itemExistente = Itens.First(i => i.ProdutoId == item.ProdutoId);
                itemExistente.Quantidade += item.Quantidade;
            }

            Itens.Add(item);

            this.ValorTotal += this.Itens.Sum(i => i.Valor);
        }

        internal void RemoverItem(ItemCarrinho item)
        {
            Itens.Remove(Itens.First(ic => ic.ProdutoId == item.ProdutoId));
        }

        internal bool ItemExiste(ItemCarrinho item)
        {
            return Itens.Any(i => i.ProdutoId == item.ProdutoId);
        }
    }

    public class CarrinhoClienteValidator : AbstractValidator<CarrinhoCliente>
    {
        public CarrinhoClienteValidator()
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
