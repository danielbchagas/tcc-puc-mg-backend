using ECommerce.Pedido.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;

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
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
        #endregion

        #region Métodos
        public ValidationResult Validar()
        {
            return new EnderecoValidator().Validate(this);
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
