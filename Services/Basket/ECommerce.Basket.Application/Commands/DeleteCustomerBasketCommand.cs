using System;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Commands
{
    public class DeleteCustomerBasketCommand : IRequest<ValidationResult>
    {
        public DeleteCustomerBasketCommand(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
    }
}
