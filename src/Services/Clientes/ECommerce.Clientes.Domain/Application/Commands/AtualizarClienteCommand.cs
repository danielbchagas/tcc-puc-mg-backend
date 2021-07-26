using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
    }
}
