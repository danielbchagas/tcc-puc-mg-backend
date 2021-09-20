using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Carrinho.Application.Commands;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carrinho.Application.Handlers.Commands
{
    public class ExcluirCarrinhoPorProdutoIdCommandHandler : IRequestHandler<ExcluirCarrinhoPorProdutoIdCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(ExcluirCarrinhoPorProdutoIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
