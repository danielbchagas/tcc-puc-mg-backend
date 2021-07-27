using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public DesativarClienteCommand(Guid id)
        {
            Id = id;
        }

        // Log do evento
        public string OrigemRequisicao { get; private set; }
        public string Uri { get; private set; }

        // Cliente
        public Guid Id { get; private set; }

        // Métodos auxiliares
        public void AdicionarOrigemRequisicao(string origemRequisicao)
        {
            OrigemRequisicao = origemRequisicao;
        }

        public void AdicionarUri(string uri)
        {
            Uri = uri;
        }
    }

    public class DesativarClienteCommandValidation : AbstractValidator<DesativarClienteCommand>
    {
        public DesativarClienteCommandValidation()
        {
            RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("{PropertyName} inválido!");
        }
    }
}
