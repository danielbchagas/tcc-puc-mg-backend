using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarTelefoneCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
