using FluentValidation;
using FluentValidation.Results;
using System;

namespace ECommerce.Pedido.Domain.Models
{
    public class Cliente : Entity
    {
        #region Construtores
        public Cliente(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public Cliente(string nome, string sobrenome, Documento documento, Email email, Telefone telefone, Endereco endereco)
        {
            Nome = nome;
            Sobrenome = sobrenome;

            Documento = documento;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
        #endregion

        #region Propriedades
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Telefone Telefone { get; set; }
        public Endereco Endereco { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new ClienteValidator().Validate(this);
        }
        #endregion
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
            RuleFor(_ => _.Documento)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(_ => _.Email)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(_ => _.Endereco)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo.");
            RuleFor(_ => _.Telefone)
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo.");
        }
    }
}
