using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Customers.Application.Commands
{
    public class DisableCustomerCommand : IRequest<ValidationResult>
    {
        public DisableCustomerCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
