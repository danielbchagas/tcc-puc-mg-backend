using FluentValidation;
using System;

namespace ECommerce.Cliente.Domain.Models
{
    public class Email : Entity
    {
        protected Email()
        {

        }

        public Email(string endereco, Guid clienteId)
        {
            Endereco = endereco;
            ClienteId = clienteId;
        }

        public string Endereco { get; private set; }
        
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
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
