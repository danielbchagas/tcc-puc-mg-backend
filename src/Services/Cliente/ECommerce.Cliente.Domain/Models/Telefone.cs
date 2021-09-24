using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Cliente.Domain.Models
{
    public class Telefone : Entity
    {
        #region Construtores
        public Telefone(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }
        #endregion

        #region Propriedades
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
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
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
