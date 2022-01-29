using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<ValidationResult>
    {
        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
