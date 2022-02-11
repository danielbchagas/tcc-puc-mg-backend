using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customer.Application.Commands
{
    public class UpdateDocumentCommand : IRequest<ValidationResult>
    {
        public UpdateDocumentCommand(Guid id, string number, Guid userId)
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
