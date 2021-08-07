using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
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
