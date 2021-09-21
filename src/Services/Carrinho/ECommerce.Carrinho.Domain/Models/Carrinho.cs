﻿using ECommerce.Carrinho.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Carrinho.Domain.Models
{
    public class Carrinho : Entity, IAggregateRoot
    {
        public Carrinho(Guid clienteId)
        {
            ClienteId = clienteId;
            Itens = new List<ItemCarrinho>();
        }

        #region Propriedades
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }

        public ICollection<ItemCarrinho> Itens { get; set; }
        #endregion

        #region Métodos
        public ValidationResult AdicionarItemAoCarrinho(ItemCarrinho item)
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
