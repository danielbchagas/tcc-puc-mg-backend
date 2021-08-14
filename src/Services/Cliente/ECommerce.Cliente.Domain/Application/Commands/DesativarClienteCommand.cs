using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public DesativarClienteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
