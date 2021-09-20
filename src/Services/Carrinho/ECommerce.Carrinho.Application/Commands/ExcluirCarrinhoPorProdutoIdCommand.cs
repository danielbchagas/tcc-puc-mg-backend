using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class ExcluirCarrinhoPorProdutoIdCommand : IRequest<ValidationResult>
    {
        public ExcluirCarrinhoPorProdutoIdCommand(Guid produtoId)
        {
            ProdutoId = produtoId;
        }

        public Guid ProdutoId { get; set; }
    }
}
