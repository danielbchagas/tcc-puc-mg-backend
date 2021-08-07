using System;
using ECommerce.Cliente.Domain.Enums;
using FluentValidation;

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
        public Cliente Cliente { get; private set; }
    }

    public class DocumentoValidator : AbstractValidator<Documento>
    {
        public DocumentoValidator()
        {
            RuleFor(d => d.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(d => d.Numero)
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString())
                .MaximumLength(18)
                .WithMessage(ErrosValidacao.MaiorQue.ToString());
            RuleFor(d => d.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
        }
    }
}
