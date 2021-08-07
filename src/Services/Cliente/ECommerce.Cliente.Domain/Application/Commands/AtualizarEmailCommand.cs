using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarEmailCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }

        public Guid ClienteId { get; set; }
    }
}
