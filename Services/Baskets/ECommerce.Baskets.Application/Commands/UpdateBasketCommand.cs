using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Basket.Application.Commands
{
    public class UpdateBasketCommand : IRequest<ValidationResult>
    {
        public UpdateBasketCommand(Guid id, bool isEnded, Guid customerId)
        {
            Id = id;
            IsEnded = isEnded;
            CustomerId = customerId;
        }

        public Guid Id { get; set; }
        public bool IsEnded { get; set; }
        public Guid CustomerId { get; set; }
    }
}
