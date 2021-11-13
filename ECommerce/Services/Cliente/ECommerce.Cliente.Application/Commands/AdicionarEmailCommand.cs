using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Application.Commands
{
    public class AdicionarEmailCommand : IRequest<ValidationResult>
    {
        public AdicionarEmailCommand(Guid id, string endereco, Guid clienteId)
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
