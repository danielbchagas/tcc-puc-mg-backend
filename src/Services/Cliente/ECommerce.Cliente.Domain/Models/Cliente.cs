using ECommerce.Cliente.Domain.Interfaces.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Cliente.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        #region Construtores
        public Cliente(Guid id, string nome, string sobrenome, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;
        }

        public Cliente(Guid id, string nome, string sobrenome, Documento documento, Email email, Telefone telefone, bool ativo = true)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Ativo = ativo;

            Documento = documento;
            Email = email;
            Telefone = telefone;
        }
        #endregion

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public bool Ativo { get; set; }

        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Telefone Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public ValidationResult Validar()
        {
            return new ClienteValidator().Validate(this);
        }
    }

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Nome)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Sobrenome)
                .MaximumLength(100)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
