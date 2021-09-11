using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarEmailCommand : IRequest<ValidationResult>
    {
        public AtualizarEmailCommand(Guid id, string endereco, Guid clienteId)
        {
            Id = id;
            Endereco = endereco;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
