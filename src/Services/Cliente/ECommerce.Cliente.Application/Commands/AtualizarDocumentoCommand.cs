using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Application.Commands
{
    public class AtualizarDocumentoCommand : IRequest<ValidationResult>
    {
        public AtualizarDocumentoCommand(Guid id, string numero, Guid clienteId)
        {
            Id = id;
            Numero = numero;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
