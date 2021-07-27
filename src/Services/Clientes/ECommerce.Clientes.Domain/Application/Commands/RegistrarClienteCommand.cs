using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class RegistrarClienteCommand : IRequest<ValidationResult>
    {
        // Log do evento
        public string OrigemRequisicao { get; set; }
        public string Uri { get; set; }

        // Cliente
        public Guid UsuarioId { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }

        // Endereco
        public Guid EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estados { get; set; }
    }

    public class RegistrarClienteCommandValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteCommandValidation()
        {
            RuleFor(_ => _.NomeFantasia).NotNull().NotEmpty().WithMessage("Nome inválido!");
            RuleFor(_ => _.Cnpj).NotNull().NotEmpty().WithMessage("Nome inválido!");
        }
    }
}
