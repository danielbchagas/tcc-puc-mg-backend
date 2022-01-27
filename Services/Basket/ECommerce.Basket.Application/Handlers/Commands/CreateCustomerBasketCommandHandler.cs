using System.Threading;
using System.Threading.Tasks;
using ECommerce.Basket.Application.Commands;
using ECommerce.Basket.Domain.Interfaces.Repositories;
using ECommerce.Basket.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Basket.Application.Handlers.Commands
{
    public class CreateCustomerBasketCommandHandler : IRequestHandler<CreateCustomerBasketCommand, ValidationResult>
    {
        private readonly ICustomerBasketRepository _cartRepository;
        
        public CreateCustomerBasketCommandHandler(ICustomerBasketRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerBasketCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult();

            var cart = new CustomerBasket(request.CustomerId);

            validation = cart.Validate();

            if (!validation.IsValid)
                return await Task.FromResult(validation);

            await _cartRepository.Create(cart);
            await _cartRepository.UnitOfWork.Commit();

            return await Task.FromResult(validation);
        }
    }
}
