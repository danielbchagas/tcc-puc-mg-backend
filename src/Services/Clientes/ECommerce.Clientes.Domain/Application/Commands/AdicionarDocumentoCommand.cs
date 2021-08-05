using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AdicionarDocumentoCommand : IRequest<ValidationResult>
    {
        public AdicionarDocumentoCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
