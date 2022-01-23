using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Carrinho.Application.Commands
{
    public class DeleteCarrinhoCommand : IRequest<ValidationResult>
    {
        public DeleteCarrinhoCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
