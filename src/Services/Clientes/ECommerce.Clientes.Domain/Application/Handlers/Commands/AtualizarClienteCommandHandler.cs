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
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
