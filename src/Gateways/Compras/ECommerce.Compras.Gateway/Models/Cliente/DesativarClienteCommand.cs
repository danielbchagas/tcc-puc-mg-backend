using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Compras.Gateway.Models.Cliente
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
