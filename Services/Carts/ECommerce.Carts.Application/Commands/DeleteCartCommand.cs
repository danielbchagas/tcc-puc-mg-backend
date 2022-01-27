using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Carts.Application.Commands
{
    public class DeleteCartCommand : IRequest<ValidationResult>
    {
        public DeleteCartCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
