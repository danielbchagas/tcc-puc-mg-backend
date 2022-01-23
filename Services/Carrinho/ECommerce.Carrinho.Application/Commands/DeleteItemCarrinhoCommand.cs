using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carrinho.Application.Commands
{
    public class DeleteItemCarrinhoCommand : IRequest<ValidationResult>
    {
        public DeleteItemCarrinhoCommand(Guid id, Guid userId)
        {
            Id = id;

            UserId = userId;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
