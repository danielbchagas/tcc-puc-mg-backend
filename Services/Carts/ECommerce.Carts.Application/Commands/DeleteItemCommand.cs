using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Commands
{
    public class DeleteItemCommand : IRequest<ValidationResult>
    {
        public DeleteItemCommand(Guid id, Guid userId)
        {
            Id = id;

            UserId = userId;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
