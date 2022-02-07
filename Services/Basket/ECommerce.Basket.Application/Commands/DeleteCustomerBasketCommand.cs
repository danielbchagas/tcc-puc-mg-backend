using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteCustomerBasketCommand : IRequest<ValidationResult>
    {
        public DeleteCustomerBasketCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
