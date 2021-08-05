using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AdicionarEmailCommand : IRequest<ValidationResult>
    {
        public AdicionarEmailCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
