using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarTelefoneCommand : IRequest<ValidationResult>
    {
        public AdicionarTelefoneCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }

        public Guid ClienteId { get; set; }
    }
}
