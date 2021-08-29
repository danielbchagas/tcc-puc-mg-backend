using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Cliente.Domain.Models
{
    public class Documento : Entity
    {
        protected Documento()
        {

        }

        public Documento(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }

        public string Numero { get; private set; }
        
        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public Cliente Cliente { get; private set; }
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
