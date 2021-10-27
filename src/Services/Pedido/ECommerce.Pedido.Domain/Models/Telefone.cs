using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Telefone : Entity
    {
        #region Construtores
        public Telefone(string numero)
        {
            Numero = numero;
        }
        #endregion

        #region Propriedades
        public string Numero { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new TelefoneValidator().Validate(this);
        }
        #endregion
    }

    public class TelefoneValidator : AbstractValidator<Telefone>
    {
        public TelefoneValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Numero)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}
