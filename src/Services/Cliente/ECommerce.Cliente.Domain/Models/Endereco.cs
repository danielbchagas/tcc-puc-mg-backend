using System;
using ECommerce.Cliente.Domain.Enums;
using FluentValidation;

namespace ECommerce.Cliente.Domain.Models
{
    public class Endereco : Entity
    {
        protected Endereco()
        {

        }

        public Endereco(string logradouro, string bairro, string cidade, string cep, Estados estado, Guid clienteId)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;
            ClienteId = clienteId;
        }

        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estado { get; private set; }

        // Relacionamento
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
    }

    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Logradouro)
                .MaximumLength(200)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Bairro)
                .MaximumLength(50)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Cidade)
                .MaximumLength(50)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Cep)
                .MaximumLength(9)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
        }
    }
}