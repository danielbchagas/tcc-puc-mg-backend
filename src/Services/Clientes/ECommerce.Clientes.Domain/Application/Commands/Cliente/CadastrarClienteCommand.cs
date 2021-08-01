using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using Dominio = ECommerce.Clientes.Domain.Models;

namespace ECommerce.Clientes.Domain.Application.Commands.Cliente
{
    public class CadastrarClienteCommand : IRequest<ValidationResult>
    {
        public CadastrarClienteCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Nascimento { get; set; }
        public Dominio.Documento Documento { get; set; }
        public Dominio.Email Email { get; set; }
        public Dominio.Endereco Endereco { get; set; }
        public bool Ativo { get; set; }
    }

    public class RegistrarClienteCommandValidation : AbstractValidator<CadastrarClienteCommand>
    {
        public RegistrarClienteCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} Inválido!");
            RuleFor(_ => _.Nome)
                .MaximumLength(100).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Documento.Numero)
                .MaximumLength(18).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
        }
    }
}
