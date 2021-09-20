using ECommerce.Carrinho.Application.Commands;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class AdicionarItemCarrinhoCommandHandler : IRequestHandler<AdicionarItemCarrinhoCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AdicionarItemCarrinhoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
