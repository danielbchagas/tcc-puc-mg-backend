using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Produto : Entity
    {
        public Produto(string nome, int quantidade, decimal valor, string imagem)
        {
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
            Imagem = imagem;
        }

        #region Propriedades
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new ProdutoValidator().Validate(this);
        }
        #endregion
    }

    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Imagem)
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio");
            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage("{PropertyName} tem um valor menor do que o esperado!")
                .LessThan(decimal.MaxValue)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}
