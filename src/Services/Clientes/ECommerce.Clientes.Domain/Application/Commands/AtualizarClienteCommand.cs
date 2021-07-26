using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        public Cliente Cliente { get; set; }
    }

    public class AtualizarClienteCommandValidation : AbstractValidator<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidation()
        {
            RuleFor(_ => _.Cliente.Id).NotEqual(Guid.Empty).WithMessage("Identificador inválido!");
            RuleFor(_ => _.Cliente.NomeFantasia).NotNull().NotEmpty().WithMessage("Nome inválido!");
            RuleFor(_ => _.Cliente.Cnpj).NotNull().NotEmpty().WithMessage("Nome inválido!");
            RuleFor(_ => _.Cliente.Endereco).NotNull().WithMessage("Nome inválido!");
        }
    }
}
