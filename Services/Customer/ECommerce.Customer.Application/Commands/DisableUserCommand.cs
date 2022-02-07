using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class DisableUserCommand : IRequest<ValidationResult>
    {
        public DisableUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
