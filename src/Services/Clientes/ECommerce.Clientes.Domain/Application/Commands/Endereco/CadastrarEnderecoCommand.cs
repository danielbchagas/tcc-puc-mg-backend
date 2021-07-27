using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands.Endereco
{
    public class CadastrarEnderecoCommand : IRequest<ValidationResult>
    {
        public CadastrarEnderecoCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
        public bool Ativo { get; set; }

        public Guid ClienteId { get; set; }
    }

    public class RegistrarEnderecoCommandValidation : AbstractValidator<CadastrarEnderecoCommand>
    {
        public RegistrarEnderecoCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} Inválido!");
            RuleFor(_ => _.Logradouro)
                .MaximumLength(200).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Bairro)
                .MaximumLength(50).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cidade)
                .MaximumLength(50).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cep)
                .MaximumLength(9).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.ClienteId).NotEqual(Guid.Empty).WithMessage("{PropertyName} inválido!");
        }
    }
}
