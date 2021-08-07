using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
    }
}
