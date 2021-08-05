using ECommerce.Clientes.Domain.Enums;
using FluentValidation;
using System;

namespace ECommerce.Clientes.Domain.Models
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
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(d => d.Endereco)
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString())
                .MaximumLength(100)
                .WithMessage(ErrosValidacao.MaiorQue.ToString());
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
        }
    }
}
