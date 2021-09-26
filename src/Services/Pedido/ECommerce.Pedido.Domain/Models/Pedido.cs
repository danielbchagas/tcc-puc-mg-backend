using ECommerce.Pedido.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Pedido.Domain.Models
{
    public class Pedido : Entity
    {
        #region Construtores
        public Pedido(StatusPedido status, Cliente cliente, IList<Produto> produtos)
        {
            Status = status;

            Cliente = cliente;
            Produtos = produtos;
        }
        #endregion

        #region Propriedades
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new PedidoValidator().Validate(this);
        }

        public void CalcularTotalPedido()
        {
            Valor = Produtos.Sum(_ => _.Quantidade * _.Valor);
        }
        #endregion
    }

    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Valor)
                .GreaterThan(0)
                .WithMessage("{PopertyName} não pode ser 0!");
            RuleFor(_ => _.Cliente)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo!");
            RuleFor(_ => _.Produtos)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo!");
        }
    }
}
