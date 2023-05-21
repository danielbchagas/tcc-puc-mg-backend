using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<ValidationResult>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
