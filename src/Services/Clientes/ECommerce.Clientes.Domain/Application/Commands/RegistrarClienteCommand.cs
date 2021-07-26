using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class RegistrarClienteCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Cliente
        public Cliente Cliente { get; set; }
    }

    public class RegistrarClienteCommandValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteCommandValidation()
        {
            RuleFor(_ => _.Cliente.NomeFantasia).NotNull().NotEmpty().WithMessage("Nome inválido!");
            RuleFor(_ => _.Cliente.Cnpj).NotNull().NotEmpty().WithMessage("Nome inválido!");
            RuleFor(_ => _.Cliente.Endereco).NotNull().WithMessage("Nome inválido!");
        }
    }
}
