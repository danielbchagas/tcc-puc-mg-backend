using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Pedido.Domain.Models
{
    public class Email : Entity
    {
        #region Construtores
        public Email(string endereco)
        {
            Endereco = endereco;
        }
        #endregion

        #region Propriedades
        public string Endereco { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public Cliente Cliente { get; private set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new EmailValidator().Validate(this);
        }

        public void VincularCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }
        #endregion
    }

    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(d => d.Endereco)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!")
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .EmailAddress();
        }
    }
}
