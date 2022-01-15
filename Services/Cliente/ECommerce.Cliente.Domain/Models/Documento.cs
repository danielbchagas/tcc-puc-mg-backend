using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Cliente.Domain.Models
{
    public class Documento : Entity
    {
        public Documento(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }

        #region Propriedades
        public string Numero { get; set; }
        
        public Guid ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
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
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
