using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarTelefoneCommand : IRequest<ValidationResult>
    {
        public AdicionarTelefoneCommand(string numero, Guid clienteId)
        {
            Numero = numero;
            ClienteId = clienteId;
        }

        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
