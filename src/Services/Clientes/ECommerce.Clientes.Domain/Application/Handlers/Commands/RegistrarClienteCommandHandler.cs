using ECommerce.Clientes.Domain.Application.Commands;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Handlers.Commands
{
    public class RegistrarClienteCommandHandler : IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
