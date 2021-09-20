using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carrinho.Application.Commands
{
    public class ExcluirItemCarrinhoPorProdutoIdCommand : IRequest<ValidationResult>
    {
        public ExcluirItemCarrinhoPorProdutoIdCommand(Guid produtoId)
        {
            ProdutoId = produtoId;
        }

        public Guid ProdutoId { get; set; }
    }
}
