using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Documento : Entity
    {
        #region Construtores
        public Documento(string numero)
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
            return new DocumentoValidator().Validate(this);
        }
        #endregion
    }

    public class DocumentoValidator : AbstractValidator<Documento>
    {
        public DocumentoValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Numero)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(18)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!");
        }
    }
}
