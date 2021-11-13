using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Cliente.Domain.Models
{
    public class Email : Entity
    {
        public Email(string endereco, Guid clienteId)
        {
            Endereco = endereco;
            ClienteId = clienteId;
        }

        #region Propriedades
        public string Endereco { get; set; }
        
        public Guid ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new EmailValidator().Validate(this);
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
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
