using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

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
        public string Numero { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public Cliente Cliente { get; private set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new TelefoneValidator().Validate(this);
        }

        public void VincularCliente(Guid clienteId)
        {
            ClienteId = clienteId;
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
