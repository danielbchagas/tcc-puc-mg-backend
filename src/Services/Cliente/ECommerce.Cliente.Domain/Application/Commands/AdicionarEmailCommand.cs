using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarEmailCommand : IRequest<ValidationResult>
    {
        public AdicionarEmailCommand(string endereco, Guid clienteId)
        {
            Endereco = endereco;
            ClienteId = clienteId;
        }

        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
