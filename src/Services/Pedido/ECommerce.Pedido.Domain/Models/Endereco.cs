using ECommerce.Pedido.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace ECommerce.Pedido.Domain.Models
{
    public class Endereco : Entity
    {
        #region Construtores
        public Endereco(string logradouro, string bairro, string cidade, string cep, Estados estado)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estado = estado;
        }
        #endregion

        #region Propriedades
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estado { get; private set; }

        public Guid ClienteId { get; private set; }
        [JsonIgnore]
        public Cliente Cliente { get; private set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new EnderecoValidator().Validate(this);
        }

        public void VincularCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }
        #endregion
    }

    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(_ => _.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Logradouro)
                .MaximumLength(200)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Bairro)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Cidade)
                .MaximumLength(50)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
            RuleFor(_ => _.Cep)
                .MaximumLength(9)
                .WithMessage("{PropertyName} tem um valor maior do que o esperado!")
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} não pode ser nulo ou vazio!");
        }
    }
}
