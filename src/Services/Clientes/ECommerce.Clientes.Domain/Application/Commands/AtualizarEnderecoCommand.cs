using ECommerce.Clientes.Domain.Enums;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarEnderecoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public Estados Estado { get; set; }
        public bool EnderecoAtivo { get; set; }

        public Guid ClienteId { get; set; }
    }
}
