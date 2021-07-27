using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands.Cliente
{
    public class CadastrarClienteCommand : IRequest<ValidationResult>
    {
        public CadastrarClienteCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
    }

    public class RegistrarClienteCommandValidation : AbstractValidator<CadastrarClienteCommand>
    {
        public RegistrarClienteCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} Inválido!");
            RuleFor(_ => _.NomeFantasia)
                .MaximumLength(100).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cnpj)
                .MaximumLength(18).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
        }
    }
}
