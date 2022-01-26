using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands
{
    public class UpdateDocumentCommand : IRequest<ValidationResult>
    {
        public UpdateDocumentCommand(Guid id, string number, Guid customerId)
        {
            Id = id;
            Number = number;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
    }
}
