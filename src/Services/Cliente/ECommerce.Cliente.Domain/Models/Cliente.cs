using System;
using ECommerce.Cliente.Domain.Enums;
using ECommerce.Cliente.Domain.Interfaces.Entities;
using FluentValidation;

namespace ECommerce.Cliente.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        protected Cliente()
        {

        }

        public Cliente(string nome, string sobrenome, DateTime dataNascimento, bool ativo = true)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Ativo { get; private set; }

        public Email Email { get; private set; }
        public Documento Documento { get; private set; }
        public Endereco Endereco { get; private set; }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void VincularEmail(Email email)
        {
            Email = email;
        }

        public void VincularDocumento(Documento documento)
        {
            Documento = documento;
        }

        public void VincularEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Nome)
                .MaximumLength(50)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(_ => _.Sobrenome)
                .MaximumLength(100)
                .WithMessage(ErrosValidacao.MaiorQue.ToString())
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
            RuleFor(c => c.DataNascimento)
                .GreaterThan(DateTime.Now)
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString())
                .NotNull()
                .WithMessage(ErrosValidacao.NuloOuVazio.ToString());
        }
    }
}
