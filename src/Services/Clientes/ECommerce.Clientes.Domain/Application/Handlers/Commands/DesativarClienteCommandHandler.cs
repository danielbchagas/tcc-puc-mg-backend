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
    public class DesativarClienteCommandHandler : IRequestHandler<DesativarClienteCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(DesativarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
