using ECommerce.Carrinho.Application.Commands;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class AtualizarCarrinhoCommandHandler : IRequestHandler<AtualizarCarrinhoCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AtualizarCarrinhoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
