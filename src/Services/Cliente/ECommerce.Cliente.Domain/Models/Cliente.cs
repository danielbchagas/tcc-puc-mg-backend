using ECommerce.Cliente.Domain.Interfaces.Entities;
using FluentValidation;
using System;

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

        public Documento Documento { get; private set; }
        public Email Email { get; private set; }
        public Telefone Telefone { get; private set; }
        public Endereco Endereco { get; private set; }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
        
        public void VincularDocumento(Documento documento)
        {
            Documento = documento;
        }

        public void VincularEmail(Email email)
        {
            Email = email;
        }

        public void VincularTelefone(Telefone telefone)
        {
            Telefone = telefone;
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
            RuleFor(c => c.DataNascimento)
                .GreaterThan(DateTime.Now)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
