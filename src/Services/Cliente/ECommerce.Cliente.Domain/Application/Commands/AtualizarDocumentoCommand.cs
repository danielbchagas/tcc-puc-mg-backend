using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AtualizarDocumentoCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
