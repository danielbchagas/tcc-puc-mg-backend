using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarDocumentoCommand : IRequest<ValidationResult>
    {
        public AdicionarDocumentoCommand(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }

        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
