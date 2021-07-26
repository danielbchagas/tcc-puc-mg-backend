using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class RegistrarClienteCommand : IRequest<ValidationResult>
    {
    }
}
