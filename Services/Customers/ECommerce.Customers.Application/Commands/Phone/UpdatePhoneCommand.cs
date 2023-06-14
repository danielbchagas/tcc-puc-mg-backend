using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands.Phone
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
