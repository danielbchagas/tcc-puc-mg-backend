using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class UpdatePhoneCommand : IRequest<ValidationResult>
    {
        public UpdatePhoneCommand(Guid id, string number, Guid userId)
        {
            Id = id;
            Number = number;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid UserId { get; set; }
    }
}
