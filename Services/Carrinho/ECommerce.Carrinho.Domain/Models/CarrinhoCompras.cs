using ECommerce.Carrinho.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Carrinho.Domain.Models
{
    public class CarrinhoCompras : Entity, IAggregateRoot
    {
        public CarrinhoCompras(Guid clienteId)
        {
            ClienteId = clienteId;
            Itens = new List<CarrinhoItem>();
        }

        #region Propriedades
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<CarrinhoItem> Itens { get; set; }
        #endregion

        #region Métodos
        public ValidationResult AdicionarItemAoCarrinho(CarrinhoItem item)
        {
            // Valida se o item é válido
            var validationResult = item.Validar();

            if (!validationResult.IsValid)
                return validationResult;

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

    public class CarrinhoValidator : AbstractValidator<CarrinhoCompras>
    {
        public CarrinhoValidator()
        {
            RuleFor(ci => ci.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");
        }
    }
}
