using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class DesativarClienteCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
    }
}
